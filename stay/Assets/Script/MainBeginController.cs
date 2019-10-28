using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBeginController : MonoBehaviour
{
    // Start is called before the first frame update
    public void GameScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void SettingScene()
    {
        SceneManager.LoadScene("SettingUI");
    }

    public void IntroduceScene()
    {
        SceneManager.LoadScene("IntroduceUI");
        Debug.Log("游戏规则/背景介绍");
    }

    public void EixtGame()
    {
        Application.Quit();
        Debug.Log("退出应用程序");
    }
}
