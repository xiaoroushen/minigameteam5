using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNoteGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> eventList;
    public float timeleftToBegin;
    public float delayTime;
    public float earlyTime;
    private int index = 0;
    private bool haveEventList;

    public AudioSource myAudioSource;
    public GameObject cubePrefab;
    void Start()
    {
        timeleftToBegin = earlyTime - delayTime;
        haveEventList= GetPreStoreEventList();
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


}
