using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    public float existedTime;
    // Start is called before the first frame update
    public float zRotateSpeed;
    void Start()
    {
        Destroy(gameObject, existedTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, zRotateSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "fish")
        {
            Debug.Log("FISH123");
            collision.SendMessage("Escape");
        }
    }

}
