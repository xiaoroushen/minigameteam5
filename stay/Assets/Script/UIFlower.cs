using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFlower : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private bool isEnd;
    public GameObject father;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckUIFlowerPosition();
    }

    private void getImpact(Vector3 force)
    {
        if (!isEnd)
        {
            rb2D.AddForce(force * 1, ForceMode2D.Impulse);
        }


    }

    private void CheckUIFlowerPosition()
    {
        if (transform.position.magnitude < 0.36)
        {
            rb2D.velocity = new Vector2(0, 0);
            isEnd = true;
            transform.parent.GetComponent<Initialize>().EndThisPlay();
        }
    }
}
