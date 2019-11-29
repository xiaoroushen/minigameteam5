using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainBeginController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SystemPanel;
    public GameObject MainPanel;
    public GameObject RulePanel;
    public Slider volumeSlider;
    private AudioSource buttonAudioSource;
    public AudioSource backgroundMusic;

    private void Awake()
    {
        buttonAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void GameScene()
    {
        buttonAudioSource.Play();
        Invoke("LoadBookScene", 0.4f);

    }
    public void SettingScene()
    {
        buttonAudioSource.Play();
        MainPanel.SetActive(false);
        SystemPanel.SetActive(true);
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }

    }

    public void IntroduceScene()
    {
        buttonAudioSource.Play();
        MainPanel.SetActive(false);
        RulePanel.SetActive(true);
    }

    public void EixtGame()
    {
        Application.Quit();
        Debug.Log("退出应用程序");
    }

    private void LoadBookScene()
    {
        SceneManager.LoadSceneAsync("Book");
    }


    public void AdjustVolume()
    {
        backgroundMusic.volume = volumeSlider.value;
    }
    
}
