using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseEventManager : MonoBehaviour
{

    private Animator anim;

    public AudioSource audioSource;
    public GameObject button;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Pause()
    {

        anim.SetBool("IsPause", true);
       
        button.SetActive(false);
    } 

    public void unPause()
    {
        Time.timeScale = 1;
        anim.SetBool("IsPause", false);
        Debug.Log("unpause");
    }

    public void Home()
    {
        SceneManager.LoadScene("MainUI");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        audioSource.UnPause();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
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
}
