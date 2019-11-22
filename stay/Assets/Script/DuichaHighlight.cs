using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuichaHighlight : MonoBehaviour
{
    private Color defaultColor;
    public float opacity;
    // Start is called before the first frame update
    void Start()
    {
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, opacity);
        }

            if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }


    
}
