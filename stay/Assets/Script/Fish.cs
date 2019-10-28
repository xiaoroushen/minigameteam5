using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform flowerTranform;

    private bool isEscaped;
    private Vector3 vector1;
    private Vector3 vector2;

    void Start()
    {
            InvokeRepeating("RotateController", 0, 1);
     

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isEscaped)
        {
            transform.Translate(vector1 * 1 * Time.fixedDeltaTime, Space.World);
        }
        else
        {
            transform.Translate(-vector1 * 10 * Time.fixedDeltaTime, Space.World);
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
        if (flowerTranform)
        {
            transform.eulerAngles = CalculateRotateAngle();
        }

    }

    private void Escape()
    {
        CancelInvoke("RotateController");
        transform.eulerAngles = new Vector3(-transform.eulerAngles.x, -transform.eulerAngles.y,0);
        isEscaped = true;
        Invoke("DestroyFish", 5);
    }

    private void DestroyFish()
    {
        Destroy(gameObject);
    }


}
