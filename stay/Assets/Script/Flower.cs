using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public float moveSpeed= 1;

    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // MoveTest();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "fish":
                Debug.Log("检测到fish");
                Destroy(gameObject);
                break;

            case "wall":
                Debug.Log("检测到wall");
                Destroy(gameObject);
                break;

            case "wave":
                Debug.Log("与waveTrigger触发");
                break;
            default:
                break;
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
}
