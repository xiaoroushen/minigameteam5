using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public int moveSpeed;
    private bool canBePressed;

    //0 perfect 1 good 2 normal 3 miss
    public GameObject[] effectPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                if (Mathf.Abs(transform.position.x - 3.3f) < 0.05)
                {
                    Instantiate(effectPrefab[0]);
                    PlayerManager.Instance.GetPerfectNote();
                }else if(Mathf.Abs(transform.position.x - 3.3f) < 0.2)
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
            PlayerManager.Instance.isGoodWave = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "PressZone")
        {
            canBePressed = false;
            Instantiate(effectPrefab[3]);
            PlayerManager.Instance.MissNote();
            PlayerManager.Instance.isGoodWave = false;
        }
    }

    private void DestroyGameObj()
    {
        if(transform.position.x> 5.66)
        {
            Destroy(gameObject);
        }
    }
}
