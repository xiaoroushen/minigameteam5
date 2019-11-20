using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float moveSpeed= 1;

    private bool isinvincible = true;
    public float invincibleTime;
    private Rigidbody2D rb2D;
    // Start is called before the first frame update

    void Start()
    {   
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTest();
        UpdatePlayerTransform();
        if (isinvincible)
        {
            DisableInvincible();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
 
        switch (collision.tag)
        {
            case "fish":
                if (!isinvincible)
                {
                    if (!collision.gameObject.GetComponent<Fish>().isEscaped)
                    {
                        Destroy(collision.gameObject);
                        GetHit();
                    }
                }
                break;


            case "wave":
                Debug.Log("与waveTrigger触发");
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "barrier")
        {
            Debug.Log("触碰到了空气墙");
        }
    }


    private void MoveTest()
    {
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (h != 0)
        {
            return;
        }
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);


    }

    private void getImpact(Vector3 force)
    {
        rb2D.AddForce(force * 1, ForceMode2D.Impulse);

    }

    private void GetHit()
    {
        PlayerManager.Instance.hitCount++;
    }

    private void DisableInvincible()
    {
        if (PlayerManager.Instance.sumTime <= 0)
        {
            if (invincibleTime > 0)
            {
                invincibleTime -= Time.deltaTime;
            }
            else
            {
                isinvincible = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
       

    }

    private void UpdatePlayerTransform()
    {
        PlayerManager.Instance.flowerTransform = gameObject.transform;
    //    Debug.Log(PlayerManager.Instance.flowerTransform);

    }
}
