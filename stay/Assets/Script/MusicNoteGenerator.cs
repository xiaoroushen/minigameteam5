using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> eventList;
    public float timeleftToBegin;
    public float delayTime;
    private float earlyTime;
    private int index = 0;
    private bool haveEventList;



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

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);
        Debug.Log(myAudioSource.time);
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


}
