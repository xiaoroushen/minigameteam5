using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cule_Long_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private float currentSpeed;
    private bool canBePressed;
    public float lengthOfTime;

    //0 hit  1 miss
    public GameObject[] effectPrefab;
    private void Awake()
    {
        //初始化长条长度
        transform.localScale = new Vector3(transform.localScale.x * lengthOfTime * moveSpeed, transform.localScale.y, transform.localScale.z);
        currentSpeed = moveSpeed;
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)||(Input.touchCount>0&&Input.GetTouch(0).phase==TouchPhase.Moved))
        {

            if (canBePressed)
            {   
                //按下减少长条常数函数
                DecreaseCubeLength();
                //if (Mathf.Abs(transform.position.x - 3.3f) < 0.05)
                //{
                //    //Instantiate(effectPrefab[0]);
                //    PlayerManager.Instance.GetPerfectNote();
                //}
                //else if (Mathf.Abs(transform.position.x - 3.3f) < 0.2)
                //{
                //    //Instantiate(effectPrefab[1]);
                //    PlayerManager.Instance.GetGoodNote();
                //}
                //else
                //{
                //    //Instantiate(effectPrefab[2]);
                //    PlayerManager.Instance.GetNormalNote();
                //}

            }
        }
        // 抬起结束触发
        if (Input.GetMouseButtonUp(0)|| (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                Instantiate(effectPrefab[0]);
                PlayerManager.Instance.GetPerfectNote();
            }
        }

        DestroyGameObj();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * currentSpeed * Time.fixedDeltaTime, Space.World);//移动
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PressZone")
        {
            canBePressed = true;
            if (PlayerManager.Instance.skillNum == 0) 
            {
                PlayerManager.Instance.skillNum++;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PressZone")
        {   
            if(transform.localScale.x > 0)
            {
                Instantiate(effectPrefab[1]);
            }
            canBePressed = false;
            currentSpeed = moveSpeed;
        }
    }

    private void DecreaseCubeLength()
    {
        //减少长度
        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x - moveSpeed * Time.deltaTime, transform.localScale.y, transform.localScale.z);
        }      
        currentSpeed = moveSpeed / 2;
    }

    private void DestroyGameObj()
    {
        if (transform.position.x > 5.66)
        {
            Destroy(gameObject);
        }
    }
}
