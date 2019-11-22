using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemController : MonoBehaviour
{
    public Slider musicSlider;
    public GameObject MainPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUserCache()
    {
        PlayerPrefs.DeleteAll();
    }

    private void StorageCurrentMusic()
    {
        PlayerPrefs.SetFloat("volume", musicSlider.value);
    }

    public void Back()
    {
        StorageCurrentMusic();
        gameObject.SetActive(false);
        MainPanel.SetActive(true);
    }
}
