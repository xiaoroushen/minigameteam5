using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFish : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform flowerTranform;

    public bool isEscaped;
    private Vector3 vector1;
    private Vector3 vector2;

    public float baseMoveSpeed = 1;
    private float moveSpeed;

    private float rate;
    private float totalTime;
    private Color colorFrom = new Color(0, 0, 0);
    private Color colorTo = new Color(1, 1, 1);
    //判断是否是鱼群的集体动作
    public bool isFishGroupState;

    private SpriteRenderer thisSpr;


    private void Awake()
    {
        thisSpr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InvokeRepeating("RotateController", 0, 1);
        // InvokeRepeating("UpdateFishSpeed", 0, 1);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isEscaped)
        {
            if (!isFishGroupState)
            {
                transform.Translate(vector1 * baseMoveSpeed * Time.fixedDeltaTime, Space.World);
            }

        }
        else
        {
            transform.Translate(-vector1 * baseMoveSpeed * 3 * Time.fixedDeltaTime, Space.World);
            ChangeColor(3);
        }


    }

    private Vector3 CalculateRotateAngle()
    {
        vector1 = (flowerTranform.position - transform.position).normalized;
        vector2 = new Vector3(1, 0, 0);
        float angle = Vector3.Angle(vector1, vector2);
        if (vector1.y < 0)
        {
            angle = -angle;
        }
        //  Debug.Log(angle);
        //  Debug.Log(vector2);

        return new Vector3(0, 0, angle);
    }

    private void RotateController()
    {
        if (flowerTranform && !isFishGroupState)
        {
            transform.eulerAngles = CalculateRotateAngle();
        }

    }

    private void Escape()
    {
        if (!isEscaped)
        {
            CancelInvoke("RotateController");
            if (flowerTranform)
            {
                vector1 = (flowerTranform.position - transform.position).normalized;
            }
            else
            {
                vector1 = new Vector3(0, 0, 0);
            }

            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 180);
            isEscaped = true;

            Destroy(gameObject, 5);
        }

    }




    //update moveSpeed function
    private void UpdateFishSpeed()
    {
        float randomNum = Random.value;
        moveSpeed = randomNum * baseMoveSpeed;
        //Debug.Log(moveSpeed);
    }

    private void ChangeColor(float changeTime)
    {
        if (totalTime < changeTime)
        {

            totalTime += Time.fixedDeltaTime;
            rate = Mathf.Sqrt(totalTime / changeTime);
            thisSpr.color = Color.Lerp(colorFrom, colorTo, rate);
        }
        else
        {
            totalTime = 0;
        }

    }
}
