using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> eventList;
    public float timeleftToBegin;
    public float delayTime;//延迟时间
    public int countDown=3;//倒计时
    private float earlyTime;//位移提前事件
    private int index = 0;
    private bool haveEventList;//时间表



    public AudioSource myAudioSource;
    public GameObject cubePrefab;
    private void Awake()
    {
        earlyTime = (3.3f-cubePrefab.transform.position.x) / cubePrefab.GetComponent<CubeController>().moveSpeed;
        timeleftToBegin = earlyTime - delayTime;
        haveEventList = GetPreStoreEventList();
    }

    void Start()
    {
        CountDown();
    }

    // Update is called once per frame
    void Update()
    {

        if (haveEventList)
        {
            if (timeleftToBegin > 0)
            {
                timeleftToBegin -= Time.deltaTime;
                if (timeleftToBegin < 0)
                {
                    myAudioSource.Play();

                }
            }

            if (Time.time > eventList[index] && index < eventList.Count - 1)
            {   
                index++;
                Instantiate(cubePrefab);
            }
        }
       

    }

    private bool GetPreStoreEventList()
    {
        int size = PlayerPrefs.GetInt("timeEventListSize");
            for (int i = 0; i < size; i++)
            {
                eventList.Add(PlayerPrefs.GetFloat("timeEventList " + i));
            }
        return size > 0;


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


}
