using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteGenerator : MonoBehaviour
{

    public float timeLeftToBegin;//音乐开始倒计时
    public float delayTime;//延迟时间
    public int countDown=3;//倒计时
    private float earlyTime;//位移提前事件
    private int index = 0;//索引

    private float gameOverCountDown;

    private float earlyDistance;//游鱼设定初判的距离
    private float fishEarlyTime;//游鱼提前时间
    private List<Vector3> positionEnum= new List<Vector3>();//游鱼初始的位置0右上1左上2左下3右下
    private int posSeed;//位置种子

    private float fishDelayTime;
    private Queue<int> fishTypeQueue = new Queue<int>();



    public string eventID;
    private Koreography playingKoreo;
    private int sampleRate;
    
    public bool isAccNote;
    public float accRate;

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

    //fish的prefab 0是单个 1是单排 2是多排
    public GameObject[] fishPrefab;
    private void Awake()
    {
        positionEnum.Clear();
        fishTypeQueue.Clear();
        eventSampleTimeList.Clear();
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
        Debug.Log(Time.timeSinceLevelLoad);
    }

    // Update is called once per frame
    void Update()
    {
        LoadSystemVolume();
        if (timeLeftToBegin > 0)
        {
            timeLeftToBegin -= Time.deltaTime;
           // Debug.Log(timeLeftToBegin);
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
            TwoDData temp = new TwoDData { beginTime = rawEvents[i].StartSample, typeEnum = rawEvents[i].GetIntValue() };
            eventSampleTimeList.Add(temp);
        }
    }
    //生成note
    private void GenerateNote()
    {
        //Debug.Log(Time.timeSinceLevelLoad * sampleRate);
        if ( index < eventSampleTimeList.Count && Time.timeSinceLevelLoad * sampleRate > eventSampleTimeList[index].beginTime )
        {   
            int cubeType= eventSampleTimeList[index].typeEnum>0 ?1:0;
            fishTypeQueue.Enqueue(eventSampleTimeList[index].typeEnum);

            GameObject Noteobj =  Instantiate(cubePrefab[cubeType]);
            if (isAccNote)
            {
                if (cubeType == 0)
                {
                    Noteobj.GetComponent<CubeController>().moveSpeed = 1*accRate;
                }
                else
                {
                    Noteobj.GetComponent<Cule_Long_Controller>().moveSpeed =1*accRate;
                    Noteobj.GetComponent<Cule_Long_Controller>().currentSpeed = Noteobj.GetComponent<Cule_Long_Controller>().moveSpeed;
                }
            }
            

            Invoke("GenerateFish", fishDelayTime);
            index++;
            //最后一个生成后延时结束
            if (index == eventSampleTimeList.Count)
            {
                Invoke("setGameOver", gameOverCountDown + 5);
            }
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


    private void setGameOver()
    {
        Destroy(PlayerManager.Instance.flowerTransform.gameObject);
        PlayerManager.Instance.isGameOver = true;

    }
    //初始化positionEnum
    private void InitPositionEnum()
    {
        positionEnum.Add(new Vector3(10, 5, 0));
        positionEnum.Add(new Vector3(-10, 5, 0));
        positionEnum.Add(new Vector3(-10, -5, 0));
        positionEnum.Add(new Vector3(10, -5, 0));

        positionEnum.Add(new Vector3(0, 10.2f, 0));
        positionEnum.Add(new Vector3(-11, 0, 0));
        positionEnum.Add(new Vector3(0, -10.2f, 0));
        positionEnum.Add(new Vector3(11, 0, 0));

        positionEnum.Add(new Vector3(-6.33f, 7.45f, 0));
        positionEnum.Add(new Vector3(-6.33f, -7.45f, 0));
        positionEnum.Add(new Vector3(6.33f, -7.45f, 0));
        positionEnum.Add(new Vector3(6.33f, 7.45f, 0));
        //positionEnum.Add(new Vector3(0, 9.7f, 0));
        //positionEnum.Add(new Vector3(-6.33f, 7.45f, 0));
        //positionEnum.Add(new Vector3(11, 0, 0));
        // 上 new Vector3(0,9.7,0);下 new Vector3(0,-9.7,0); -45 new new Vector3(-6.33,7.45,0)  左 - 11 右 11
    }

    private void InitTimeLeftToBegin()
    {
        Debug.Log(cubePrefab[0].GetComponent<CubeController>().moveSpeed);
        if (!isAccNote)
        {
            earlyTime = (3.3f - cubePrefab[0].transform.position.x) / cubePrefab[0].GetComponent<CubeController>().moveSpeed;
        }
        else
        {
            earlyTime = (3.3f - cubePrefab[0].transform.position.x) / (cubePrefab[0].GetComponent<CubeController>().moveSpeed*accRate);
        }
        
        timeLeftToBegin = earlyTime - delayTime;
        gameOverCountDown = timeLeftToBegin;
        Debug.Log(timeLeftToBegin);
    }

    private void InitFishEarlyTime()
    {
        earlyDistance = 3f * Mathf.Sqrt(5);
        Debug.Log(earlyDistance);
        fishEarlyTime = earlyDistance/ fishPrefab[0].GetComponent<Fish>().baseMoveSpeed;
        Debug.Log(fishEarlyTime);
    }

    private void GenerateFish()
    {
        int fishType = fishTypeQueue.Dequeue();
        posSeed = (posSeed+1) % positionEnum.Count;
        Instantiate(fishPrefab[fishType], positionEnum[posSeed], Quaternion.identity);
    }

    private void LoadSystemVolume()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            myAudioSource.volume = PlayerPrefs.GetFloat("volume");
        }
    }

}
