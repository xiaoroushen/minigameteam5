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

    private int normalHitScore = 25;
    private int goodHitScore = 50;
    private int perfectHitScore = 75;


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

    private void GetNote()
    {
        if (multiplier<=multiplierThresHold.Length)
        {
            multiplierTrack++;
            
            if (multiplierTrack >= multiplierThresHold[multiplier - 1])
            {
                multiplierTrack = 0;
                multiplier++;

            }
        }

        

    }

    public void GetNormalNote()
    {
        playerScore += multiplier * normalHitScore;
        GetNote();
    }

    public void GetGoodNote()
    {
        playerScore += multiplier * goodHitScore;
        GetNote();
    }

    public void GetPerfectNote()
    {
        playerScore += multiplier * perfectHitScore;
        GetNote();
    }

    public  void MissNote()
    {
        multiplierTrack = 0;
        multiplier = 1;

    }

}
