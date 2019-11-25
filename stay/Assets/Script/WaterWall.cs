using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWall : MonoBehaviour
{
    // Start is called before the first frame update
    //水墙生存的最大时间
    public float existedTime;
    //水墙的生命值
    public float lifeValue;
    //水墙的当前生命值
    private float currentLV;
    //墙的初始厚度
    private float originScaleOfY;
    //相对矢量
    private Vector3 relativePosition;

    private void Awake()
    {
        originScaleOfY = transform.localScale.y;
        currentLV = lifeValue;
        relativePosition = transform.position - transform.GetChild(0).position;
    }
    void Start()
    {
        
        Destroy(gameObject, existedTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "fish")
        {   
            updateState();

            
            if (collision.transform.parent != null)
            {
                collision.transform.parent.gameObject.GetComponent<Fish_Group_Controller>().ChangeState();
            }
            collision.SendMessage("Escape");

        }
    }

    private void updateState()
    {
        currentLV--;
        if (currentLV > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, originScaleOfY*(currentLV/lifeValue), transform.localScale.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void updatePosition(Vector3 startPositin)
    {
        transform.position = relativePosition + startPositin;
    }

}
