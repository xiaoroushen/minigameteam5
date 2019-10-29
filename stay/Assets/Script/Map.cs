using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] resPrefabs;  //预制体数组 0鱼 1波纹 2花
    void Start()
    {
        InitMapPrefabs();
        InvokeRepeating("createFish", 8, 8);

    }

    // Update is called once per frame
    void Update()
    {
        createWave();
    }
    //封装初始化函数
    private void CreatItem(GameObject creatGameObject, Vector3 creaePosition, Quaternion creatRotation)
    {
        GameObject itemObj = Instantiate(creatGameObject, creaePosition, creatRotation);
        itemObj.transform.SetParent(gameObject.transform);
    }

    private void InitMapPrefabs()
    {
        CreatItem(resPrefabs[2], new Vector3(0, 0, 0), Quaternion.identity);
        CreatItem(resPrefabs[0], new Vector3(-10,5, 0), Quaternion.identity);
        CreatItem(resPrefabs[0], new Vector3(10, 5, 0), Quaternion.identity);
        CreatItem(resPrefabs[0], new Vector3(-10, -5, 0), Quaternion.identity);
        CreatItem(resPrefabs[0], new Vector3(10, -5, 0), Quaternion.identity);
    }

    private void createWave()
    {
        if (!PlayerManager.Instance.isGameOver) 
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject wave = Instantiate(resPrefabs[1], new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0), Quaternion.identity);
                if (PlayerManager.Instance.isGoodWave)
                {
                    wave.GetComponent<Wave>().hasNoBadEffect = true;
                    Debug.Log("this is a good wave");
                }
            }
        }
        
    }

    private void createFish()
    {
        Random.InitState(1);
        int seed = Random.Range(0, 4);
        switch (seed)
        {
            case 0:
                CreatItem(resPrefabs[0], new Vector3(-10, -5, 0), Quaternion.identity);
                break;
            case 1:
                CreatItem(resPrefabs[0], new Vector3(-10, 5, 0), Quaternion.identity);
                break;
            case 2:
                CreatItem(resPrefabs[0], new Vector3(10, -5, 0), Quaternion.identity);
                break;
            case 3:
                CreatItem(resPrefabs[0], new Vector3(10, 5, 0), Quaternion.identity);
                break;
            default:
                break;

        }
    }

}
