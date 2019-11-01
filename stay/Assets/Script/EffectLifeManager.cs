using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float exitedTime=0.5f;
    void Start()
    {
        Destroy(gameObject, exitedTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
