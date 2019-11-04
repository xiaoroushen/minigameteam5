using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public void Restart()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    public void Next()
    {
        //记录当前通关场景playerprefab对象持久化


        SceneManager.LoadScene("Book");
    }

    public void Home()
    {
        SceneManager.LoadScene("MainUI");
    }
}
