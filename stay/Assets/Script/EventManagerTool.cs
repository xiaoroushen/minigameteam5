using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerTool : MonoBehaviour
{

    public AudioSource audioSource;

    [SerializeField]
    private List<float> eventList1;
    private int list1Index;
    private bool oldListFlag = false;   //old list loaded flag
    public float previousDelay; //add delay to the events to call event before time
    void Start()
    {
        eventList1 = new List<float>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventList1.Add(audioSource.time - previousDelay);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveList();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadList();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteList();
        }

        if (oldListFlag && audioSource.isPlaying && list1Index < eventList1.Count)
        {
            if ((audioSource.time - previousDelay) == eventList1[list1Index])
            {
                Debug.Log("Event List 1 ");
                list1Index++;
            }
        }
    }


    private void SaveList()
    {
        PlayerPrefs.SetInt("timeEventListSize", eventList1.Count);
        for (int i = 0; i < eventList1.Count; i++)
        {
            PlayerPrefs.SetFloat("timeEventList " + i, eventList1[i]);
        }
        Debug.Log("List Saved");

    }

    private void LoadList()
    {
        if (PlayerPrefs.HasKey("timeEventListSize"))
        {
            Debug.Log("Old list found...");
            int size = PlayerPrefs.GetInt("timeEventListSize");

            for (int i = 0; i < size; i++)
            {
                eventList1.Add(PlayerPrefs.GetFloat("timeEventList " + i));
            }
            oldListFlag = true;

        }
        else
        {
            Debug.Log("Old list not found...");
            oldListFlag = false;

        }
        Debug.Log("List Loaded");


    }

    private void DeleteList()
    {
        for (int i = 0; i < eventList1.Count; i++)
        {
            PlayerPrefs.DeleteKey("timeEventList " + i);
        }
        PlayerPrefs.DeleteKey("timeEventListSize");
        eventList1.Clear();
        Debug.Log("List Deleted");

    }
}
