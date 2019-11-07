﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    //扩散相关变量
    private float scaleIncRate;
    public float existedTime;
    public float aclRate=1;

    //是否对莲心造成副作用
    public bool hasNoBadEffect;

    //渐变相关变量
    private Color colorFrom;
    private Color colorTo;
    private bool endFade;
    private float totalTime;
    private float rate;

    private Vector3 forceDirection;

    private SpriteRenderer thisSr;
    private void Awake()
    {
        thisSr = GetComponent<SpriteRenderer>();
        colorFrom = thisSr.color;
        colorTo = new Color(colorFrom.r, colorFrom.g, colorFrom.b, 0.1f);
}
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {   
        if(existedTime> scaleIncRate)
        {
            IncreaseScale();
            FadeEffect();

        }
        else
        {
            Destroy(gameObject);
        }

        if (!endFade)
        {
            FadeEffect();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "fish":
                Debug.Log("检测到fish");
                collision.SendMessage("Escape");
                break;

            case "wall":
                Debug.Log("检测到wall");
                Destroy(gameObject);
                break;

            case "flower":
                if (!hasNoBadEffect)
                {
                    //生命属性减少
                }
                Debug.Log("与flower");

                forceDirection = collision.transform.position - transform.position;
                forceDirection=forceDirection.normalized*(1-rate);
 
                collision.SendMessage("getImpact", forceDirection);

                break;
            default:
                break;
        }
    }



    private void IncreaseScale()
    {
        scaleIncRate += Time.fixedDeltaTime;
        gameObject.transform.localScale = new Vector3(Mathf.Sqrt(scaleIncRate)* aclRate, Mathf.Sqrt(scaleIncRate)* aclRate, 0);
    }

    private void FadeEffect()
    {
        if (totalTime < existedTime)
        {
            totalTime += Time.fixedDeltaTime;
            rate = totalTime / existedTime;
            thisSr.color = Color.Lerp(colorFrom, colorTo, rate);
        }
        else
        {
            endFade = true;
        }
    }



}
