using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Group_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform flowerTranform;

    private bool groupDismiss;

    private Vector3 vector1;
    private Vector3 vector2;
   
    public float baseMoveSpeed = 1;

    private float destroyFishGroupTime;
    private float groupSwarmDistance;

    private List<GameObject> subFishList =new List<GameObject>();

    //判断是否是鱼群的集体动作



    private void Awake()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                subFishList.Add(transform.GetChild(i).gameObject);
            }
        }
        groupSwarmDistance = 3 * Mathf.Sqrt(5);
    }

    void Start()
    {

        InvokeRepeating("RotateController", 0, 1);
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFlowerTransform();
        if (groupSwarmDistance > 0)
        {
            groupSwarmDistance -= Time.deltaTime * baseMoveSpeed;
            if (groupSwarmDistance < 0)
            {
                ChangeState();
                groupSwarmDistance = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!groupDismiss)
        {
            transform.Translate(vector1 * baseMoveSpeed * Time.fixedDeltaTime, Space.World);
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

        return new Vector3(0, 0, angle);
    }

    private void RotateController()
    {
        if (flowerTranform)
        {
            transform.eulerAngles = CalculateRotateAngle();
        }

    }




    private void UpdateFlowerTransform()
    {
        if (PlayerManager.Instance.flowerTransform != null)
        {
            flowerTranform = PlayerManager.Instance.flowerTransform;
        }
    }



    public void ChangeState()
    {
        groupDismiss = true;
        for (int i = 0;i< subFishList.Count;i++)
        {
            if (subFishList[i])
            {
                subFishList[i].GetComponent<Fish>().isFishGroupState = false;
            }
        }
 
    }

    

    //如果该物体子物体都不存在，生命周期结束
    //private void DestroyGameObjectListener()
    //{
    //    if (transform.childCount == 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}



}
