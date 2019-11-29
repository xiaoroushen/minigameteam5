using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    //0为波纹 1为花
    public GameObject[] preFabs;
    // Start is called before the first frame update
    private GameObject flowerReference;
    public Transform waveInitTransform;
    public AudioSource myAudioSource;

    void Start()
    {
        Invoke("GenerateWave", 5.5f);
        myAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void EndThisPlay()
    {
        SceneManager.LoadScene("MainUI");
    }

    private void GenerateWave()
    {
        Instantiate(preFabs[0], waveInitTransform.position, Quaternion.identity);
        myAudioSource.Play();
    }

}
