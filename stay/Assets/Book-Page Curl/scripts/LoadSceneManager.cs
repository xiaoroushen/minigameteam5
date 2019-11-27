using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject lockImage;
    public GameObject buttonObj;
    private int currentPage;
    public AudioSource pianoSource;
    public GameObject levelButton;
    private bool isCheckButton;
    public Transform position1;

    public Transform position2;

    public Transform position3;
    // Update is called once per frame
    private void Start()
    { 
        if(PlayerPrefs.HasKey("currentPage"))
        {
            transform.GetChild(0).gameObject.GetComponent<Book>().currentPage = PlayerPrefs.GetInt("currentPage", currentPage);
        }



    }
    void Update()
    {
        UpdateLockStatus();
        UpdateButtonPosition();
    }
    public void LoadGameScene()
    {
        pianoSource.Play();
       
        PlayerPrefs.SetInt("currentPage", currentPage);
        SceneManager.LoadSceneAsync("Game"+ currentPage/2);
        
    }

    public void UpdateLockStatus()
    {
        currentPage = transform.GetChild(0).gameObject.GetComponent<Book>().currentPage;
        if(currentPage > 0)
        {
            if (PlayerPrefs.GetInt("Game" + currentPage/2) == 1)
            {
                lockImage.SetActive(false);
                buttonObj.GetComponent<Button>().interactable = true;
            }
            else
            {
                //lockImage.SetActive(true);
                //buttonObj.GetComponent<Button>().interactable = false;
            }

        }
        else
        {
            lockImage.SetActive(false);
            buttonObj.GetComponent<Button>().interactable = true;
        }

        if (!isCheckButton)
        {
            CheckNewEntry();
            isCheckButton = true;
        }




    }

    private void UpdateButtonPosition()
    {
        switch (currentPage)
        {
            case 2:
                levelButton.transform.position = position1.position;
                Debug.Log("2");
                break;
            case 4:
                levelButton.transform.position = position2.position;
                Debug.Log("4");
                break;
            case 6:
                levelButton.transform.position = position3.position;
                Debug.Log("6");
                break;
            default:
                break;

        }

    }

    private void CheckNewEntry()
    {
        Debug.Log(currentPage == 2 || currentPage == 4 || currentPage == 6);
        if (currentPage == 2 || currentPage == 4 || currentPage == 6)
        {
            Invoke("SetEntryActive", 2);
        }
    }

    private void SetEntryActive()
    {
        levelButton.SetActive(true);
    }

    public void LoadMainUI()
    {
        SceneManager.LoadSceneAsync("MainUI");
    }

}
