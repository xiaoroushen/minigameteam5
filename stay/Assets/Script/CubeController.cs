﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float moveSpeed=1;
    private bool canBePressed;
    private bool isEliminate;
    //0 perfect 1 good 2 normal 3 miss
    public GameObject[] effectPrefab;


    public GameObject mapPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (PlayerManager.Instance.IsPointerOverUIObject(new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
            {
                return;
            }
            if (canBePressed && (PlayerManager.Instance.cubePool.Peek() == gameObject.GetInstanceID()))
            {   

                Invoke("DequeGameObject", Time.deltaTime);//在下一帧之前出队列

                isEliminate = true;
                mapPrefab.GetComponent<Map>().CreateWave();
                gameObject.SetActive(false);
                if (Mathf.Abs(transform.position.x - 3.3f) < 0.05)
                {
                    Instantiate(effectPrefab[0]);
                    PlayerManager.Instance.GetPerfectNote();
                }
                else if (Mathf.Abs(transform.position.x - 3.3f) < 0.2)
                {
                    Instantiate(effectPrefab[1]);
                    PlayerManager.Instance.GetGoodNote();
                }
                else
                {
                    Instantiate(effectPrefab[2]);
                    PlayerManager.Instance.GetNormalNote();
                }

            }
        }

        DestroyGameObj();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PressZone")
        {
            canBePressed = true;
            PlayerManager.Instance.cubePool.Enqueue(gameObject.GetInstanceID());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PressZone")
        {
            canBePressed = false;
            if (!isEliminate)
            {
                PlayerManager.Instance.cubePool.Dequeue();
            }

            Instantiate(effectPrefab[3]);
            PlayerManager.Instance.MissNote();
        }
    }

    private void DestroyGameObj()
    {
        if (transform.position.x > 5.66)
        {
            Destroy(gameObject);
        }
    }

    private void DequeGameObject()
    {
        PlayerManager.Instance.cubePool.Dequeue();
    }

}
