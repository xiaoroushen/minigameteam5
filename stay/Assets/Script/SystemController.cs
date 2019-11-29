using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemController : MonoBehaviour
{
    public Slider musicSlider;
    public GameObject mainPanel;
    public GameObject rulePanel;
    public AudioSource buttonAudioSource;
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
        buttonAudioSource.Play();
        PlayerPrefs.DeleteAll();
    }

    private void StorageCurrentMusic()
    {
        PlayerPrefs.SetFloat("volume", musicSlider.value);
    }

    public void Back()
    {
        buttonAudioSource.Play();
        StorageCurrentMusic();
        gameObject.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void BackMain()
    {
        buttonAudioSource.Play();
        rulePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ReleseAllLevel()
    {
        buttonAudioSource.Play();
        PlayerPrefs.SetInt("Game1", 1);
        PlayerPrefs.SetInt("Game2", 1);
        PlayerPrefs.SetInt("Game3", 1);
        PlayerPrefs.SetInt("Game4", 1);
    }

    public void PlayVoice()
    {
        buttonAudioSource.Play();
    }

}
