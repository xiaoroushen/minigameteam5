using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseEventManager : MonoBehaviour
{

    private Animator anim;

    public AudioSource audioSource;
    public GameObject button;
    public AudioSource buttonAS;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Pause()
    {
        buttonAS.Play();
        anim.SetBool("IsPause", true);
       
        button.SetActive(false);
    } 

    public void unPause()
    {
        buttonAS.Play();
        Time.timeScale = 1;
        anim.SetBool("IsPause", false);
        audioSource.UnPause();
        Debug.Log("unpause");
    }

    public void Home()
    {
        buttonAS.Play();
        Time.timeScale = 1;
        Invoke("LoadHome", 0.4f);
    }

    public void Restart()
    {
        buttonAS.Play();
        Time.timeScale = 1;
        Invoke("RestartButton", 0.4f);
    }


    public void EndPauseAnimation()
    {
        Time.timeScale = 0;
        audioSource.Pause();

    }

    public void EndunPauseAnimation()
    {
        
        button.SetActive(true);
    }

    private void RestartButton()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void LoadHome()
    {
        SceneManager.LoadSceneAsync("MainUI");
    }
}
