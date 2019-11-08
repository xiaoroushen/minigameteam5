using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeLeftToBegin;
    public float delayTime;//延迟时间
    public int countDown=3;//倒计时
    private float earlyTime;//位移提前事件
    private int index = 0;


    public string eventID;
    private Koreography playingKoreo;
    private int sampleRate;
    private List<int> eventSampleTimeList = new List<int>();

    private AudioSource myAudioSource;

    public GameObject cubePrefab;
    private void Awake()
    {
        earlyTime = (3.3f-cubePrefab.transform.position.x) / cubePrefab.GetComponent<CubeController>().moveSpeed;
        timeLeftToBegin = earlyTime - delayTime;

        myAudioSource = gameObject.GetComponent<AudioSource>();

        playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);
        sampleRate = playingKoreo.SampleRate;
        LoadEventSampleTime();
    }

    void Start()
    {
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {

        if (timeLeftToBegin > 0)
        {
            timeLeftToBegin -= Time.deltaTime;
            //Debug.Log(timeLeftToBegin);
            if (timeLeftToBegin <= 0)
            {
                myAudioSource.Play();
                timeLeftToBegin = 0;
            }
        }
        GenerateNote();





    }
    //获取注册时间列表
    private void LoadEventSampleTime()
    {
        KoreographyTrack rhythmTrack = playingKoreo.GetTrackByID(eventID);
        List<KoreographyEvent> rawEvents = rhythmTrack.GetAllEvents();
        for (int i = 0; i < rawEvents.Count; i++)
        {
            eventSampleTimeList.Add(rawEvents[i].StartSample);
        }
    }
    //生成note
    private void GenerateNote()
    {
        Debug.Log(Time.time * sampleRate);

        if (Time.time * sampleRate > eventSampleTimeList[index] && index < eventSampleTimeList.Count - 1)
        {
            index++;
            Instantiate(cubePrefab);
        }
    }



    private IEnumerator CountDown()
    {
        while (countDown >= 0)
        {
            if (countDown > 0)
            {
                countDown--;
                Debug.Log(Time.time);
                yield return new WaitForSeconds(1);
            }
        }
        yield break;
        


    }


    private void IsGameOver()
    {
        if (myAudioSource.isPlaying == false)
        {
            Invoke("setGameOver", 3);
        }
    }

    private void setGameOver()
    {
        PlayerManager.Instance.isGameOver = true;

    }

}
