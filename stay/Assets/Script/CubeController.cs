using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public int moveSpeed;
    private bool canBePressed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGame",10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canBePressed)
            {
                Destroy(gameObject);
                PlayerManager.Instance.GetNote();
            }
        }
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
            PlayerManager.Instance.MissNote();
            PlayerManager.Instance.isGoodWave = false;
        }
    }

    private void DestroyGame()
    {
        Destroy(gameObject);
    }
}
