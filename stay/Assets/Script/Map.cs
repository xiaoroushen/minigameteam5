using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] resPrefabs;  //预制体数组 0鱼 1波纹 2花
    void Start()
    {
        InitMapPrefabs();
        //InvokeRepeating("createFish", 8, 8);

        //测试场景切换
       // Invoke("TestScene", 2);

    }

    // Update is called once per frame
    void Update()
    {   
       // createWave();
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

    }

    public void CreateWave()
    {
        if (!PlayerManager.Instance.isGameOver&&PlayerManager.Instance.sumTime<=0) 
        {   

                Debug.Log(PlayerManager.Instance.sumTime);
                if(EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject wave = Instantiate(resPrefabs[1], new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0), Quaternion.identity);
         
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

    void OnTap(TapGesture gesture)
    {
        //CreateWave();

    }

    void OnCustomGesture(PointCloudGesture gesture)
    {
        Debug.Log(gesture.RecognizedTemplate.name);
        if(PlayerManager.Instance.skillNum>0){
            if (gesture.RecognizedTemplate.name == "yuan")
            {
                Debug.Log(gesture.RecognizedTemplate.name);
                Instantiate(resPrefabs[4], Camera.main.ScreenToWorldPoint(new Vector3(gesture.Position.x, gesture.Position.y, 10)), Quaternion.identity);

            }
            else
            {
                float angle = VectorAngle(gesture.Position - gesture.StartPosition, new Vector2(1, 0));
                Instantiate(resPrefabs[3], Camera.main.ScreenToWorldPoint(new Vector3(gesture.Position.x, gesture.Position.y, 10)), Quaternion.Euler(new Vector3(0, 0, angle)));
            }
            PlayerManager.Instance.skillNum--;
        }        
    }

    private float VectorAngle(Vector2 from, Vector2 to)
    {
        float angle;

        Vector3 cross = Vector3.Cross(from, to);
        angle = Vector2.Angle(from, to);
        return cross.z > 0 ? -angle : angle;
    }




    private void TestScene()
    {
        SceneManager.LoadScene("Book");
    }
}
