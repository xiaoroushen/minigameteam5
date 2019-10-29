using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int lifeValue = 3;
    public int playerScore = 0;

    public bool isDead;
    public bool isGoodWave;
    public bool isGameOver;
    public Transform flowerTransform;

    public GameObject flowerPrefab;

    public int baseGetScore = 100;
    public int multiplier ;
    public int multiplierTrack;
    public int[] multiplierThresHold;


    public Text lifeText;
    public Text scoreText;
    public Text multipulText;
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        multiplierTrack = 0;
        multiplier = 1;
    }


    private void Update()
    {
        if (isDead)
        {
            Recover();
            
        }

        RefreshUIElement();



    }

    private void Recover()
    {
        if (lifeValue <= 0)
        {
            isGameOver = true;
        }
        else 
        { 
            lifeValue--;
            Instantiate(flowerPrefab);
            isDead = false;
        }
    }

    private void RefreshUIElement()
    {
        lifeText.text = lifeValue.ToString();
        scoreText.text = playerScore.ToString();
        multipulText.text = multiplier.ToString();

    }

    public void GetNote()
    {
        if (multiplier<=multiplierThresHold.Length)
        {
            multiplierTrack++;
            Debug.Log(multiplierTrack+":" +multiplier);
            
            if (multiplierTrack >= multiplierThresHold[multiplier - 1])
            {
                multiplierTrack = 0;
                multiplier++;

            }
        }

        playerScore += multiplier * baseGetScore;

    }

    public  void MissNote()
    {
        multiplierTrack = 0;
        multiplier = 1;

    }

}
