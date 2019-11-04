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
    // character's position
    public Transform flowerTransform;

    public GameObject flowerPrefab;

    // different point of hit area 
    private int normalHitScore = 25;
    private int goodHitScore = 50;
    private int perfectHitScore = 75;

    // multiplier variable
    public int multiplier ;
    public int multiplierTrack;
    public int[] multiplierThresHold;

    //countdown variable
    public float sumTime;
    public Text countDown;

    // display component
    public Text lifeText;
    public Text scoreText;
    public Text multipulText;

    //
    public GameObject sumaryPanel;

    //single pattern
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }



    private void Awake()
    {
        Instance = this;
    }



    private void Start()
    {
        StartCoroutine("startCountDown");
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

        ShowSummaryPanel();


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
    //countDown 
    private IEnumerator startCountDown()
    {
        while(sumTime >= -1)
        {
         
            if(sumTime > 0)
            {
                countDown.text = sumTime.ToString();
                sumTime--;
                yield return new WaitForSeconds(1);

            }else if(sumTime == 0)
            {
                countDown.text = "开始";
                sumTime--;
                yield return new WaitForSeconds(1);
            }
            else
            {
                countDown.gameObject.SetActive(false);
                yield break;
            }
        }
    } 

    private void ShowSummaryPanel()
    {
        if (isGameOver == true)
        {
            sumaryPanel.SetActive(true);
        }

    }

}
