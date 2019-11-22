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
    public Slider volumeSlider;

    public void GameScene()
    {
        SceneManager.LoadScene("Book");
    }
    public void SettingScene()
    {
        MainPanel.SetActive(false);
        SystemPanel.SetActive(true);
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        }

    }

    public void IntroduceScene()
    {
        Debug.Log("游戏规则/背景介绍");
    }

    public void EixtGame()
    {
        Application.Quit();
        Debug.Log("退出应用程序");
    }

    private void Start()
    {
    }

    
}
