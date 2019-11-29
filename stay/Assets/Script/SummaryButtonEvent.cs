using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryButtonEvent : MonoBehaviour
{
    public AudioSource buttonAS;
    public void Restart()
    {
        buttonAS.Play();
        Invoke("LoadRestart", 0.4f);
    }

    // Update is called once per frame
    public void Next()
    {
        //记录当前通关场景playerprefab对象持久化
        buttonAS.Play();
        Invoke("LoadNext", 0.4f);
    }

    public void Home()
    {
        buttonAS.Play();
        Invoke("LoadHome", 0.4f);
    }

    private void LoadHome()
    {
        SceneManager.LoadSceneAsync("MainUI");
    }

    private void LoadNext()
    {
        SceneManager.LoadSceneAsync("Book");
    }

    private void LoadRestart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
