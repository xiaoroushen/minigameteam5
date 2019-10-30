﻿using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        UpdateLockStatus();
    }
    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("currentPage", currentPage);
        SceneManager.LoadScene("Game");
        
    }

    public void UpdateLockStatus()
    {
        currentPage = transform.GetChild(0).gameObject.GetComponent<Book>().currentPage;
        if(currentPage > 0)
        {
            if (PlayerPrefs.GetInt("level" + currentPage) == 1)
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

    }


}
