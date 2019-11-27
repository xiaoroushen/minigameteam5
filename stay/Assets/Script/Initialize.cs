using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    //0为波纹 1为花
    public GameObject[] preFabs;
    // Start is called before the first frame update
    private GameObject flowerReference;


    void Start()
    {
        flowerReference = Instantiate(preFabs[1], new Vector3(-5, 0, 0), Quaternion.identity);
        flowerReference.transform.SetParent(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(preFabs[0],new Vector3(temp.x, temp.y),Quaternion.identity);
        }
    }

    public void EndThisPlay()
    {
        SceneManager.LoadSceneAsync("MainUI");

    }


}
