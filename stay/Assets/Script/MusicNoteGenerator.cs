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

    private float earlyDistance;//游鱼设定初判的距离
    private float fishEarlyTime;//游鱼提前时间
    private List<Vector3> positionEnum= new List<Vector3>();//游鱼初始的位置0右上1左上2左下3右下
    private int posSeed;//位置种子

    private float fishDelayTime;



    public string eventID;
    private Koreography playingKoreo;
    private int sampleRate;



    //自定义二元数据结构
    struct TwoDData
    {
        public int beginTime;
        //0短cube_单个鱼 1长cube_鱼墙 2长cube_鱼排
        public int typeEnum;
    }

    private List<TwoDData> eventSampleTimeList = new List<TwoDData>();
    
    

    private AudioSource myAudioSource;
    //cubePrefab 0是小cube 1是长cube
    public GameObject[] cubePrefab;
    //fish的prefab
    public GameObject fishPrefab;
    private void Awake()
    {
        InitPositionEnum();
        InitTimeLeftToBegin();
        InitFishEarlyTime();
        fishDelayTime = timeLeftToBegin - fishEarlyTime;

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
            Debug.Log(rawEvents[i].GetIntValue());
            TwoDData temp = new TwoDData { beginTime = rawEvents[i].StartSample, typeEnum = rawEvents[i].GetIntValue() };
            eventSampleTimeList.Add(temp);
        }
    }
    //生成note
    private void GenerateNote()
    {
        //Debug.Log(Time.time * sampleRate);

        if (Time.time * sampleRate > eventSampleTimeList[index].beginTime && index < eventSampleTimeList.Count - 1)
        {   
            int cubeType= eventSampleTimeList[index].typeEnum>0 ?1:0;
            Instantiate(cubePrefab[cubeType]);
            Invoke("GenerateFish", fishDelayTime);
            index++;
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
    //初始化positionEnum
    private void InitPositionEnum()
    {
        positionEnum.Add(new Vector3(10, 5, 0));
        positionEnum.Add(new Vector3(-10, 5, 0));
        positionEnum.Add(new Vector3(-10, -5, 0));
        positionEnum.Add(new Vector3(10, -5, 0));
    }

    private void InitTimeLeftToBegin()
    {
        earlyTime = (3.3f - cubePrefab[0].transform.position.x) / cubePrefab[0].GetComponent<CubeController>().moveSpeed;
        timeLeftToBegin = earlyTime - delayTime;
        Debug.Log(timeLeftToBegin);
    }

    private void InitFishEarlyTime()
    {
        earlyDistance = 2.5f * Mathf.Sqrt(5);
        fishEarlyTime = earlyDistance/ fishPrefab.GetComponent<Fish>().baseMoveSpeed;
        Debug.Log(fishEarlyTime);
    }

    private void GenerateFish()
    {
        posSeed = (posSeed+1) % 4;
        Debug.Log(posSeed);
        Instantiate(fishPrefab, positionEnum[posSeed], Quaternion.identity);
    }

}
