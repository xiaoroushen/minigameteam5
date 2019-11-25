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
    public GameObject levelEntry;
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
    }
    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("currentPage", currentPage);

        SceneManager.LoadScene("Game"+ currentPage/2);
        
    }

    public void UpdateLockStatus()
    {
        currentPage = transform.GetChild(0).gameObject.GetComponent<Book>().currentPage;
        if(currentPage > 0)
        {
            if (PlayerPrefs.GetInt("Game" + currentPage/2) == 1)
            {
                Debug.Log("123");
                lockImage.SetActive(false);
                buttonObj.GetComponent<Button>().interactable = true;
            }
            else
            {
                Debug.Log("456");
                lockImage.SetActive(true);
                buttonObj.GetComponent<Button>().interactable = false;
            }

        }
        else
        {
            Debug.Log("123");
            lockImage.SetActive(false);
            buttonObj.GetComponent<Button>().interactable = true;
        }

        if(currentPage==2 || currentPage==4 || currentPage == 6)
        {
            levelEntry.SetActive(true);
        }
        else
        {
            levelEntry.SetActive(false);
        }

    }




}
