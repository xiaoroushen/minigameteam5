using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int playerScore = 0;
    public int hitCount=0;

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

    // position controll variable
    private bool isInEdgeArea;
    private int centreAreaCount;
    private int edgeAreaCount;
    private float centreArearate;

    //countdown variable
    public float sumTime;
    public Text countDown;

    // display component
    public Text hitCountText;
    public Text scoreText;
    public Text positionText;


    //cube对象队列
    public Queue<int> cubePool;
    //技能点数long_cube
    public int skillNum;



    //
    public GameObject sumaryPanel;

    public SpriteRenderer positionStar;
    public SpriteRenderer rhythmStar;
    public SpriteRenderer hitStar;

    public Text sumary;
 


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

        cubePool = new Queue<int>();

        multiplierTrack = 0;
        multiplier = 1;
    }


    private void Update()
    {
        UpdatePositionState();
        CalculatePosition();

        RefreshUIElement();

        ShowSummaryPanel();




    }



    private void RefreshUIElement()
    {
        hitCountText.text = hitCount.ToString();
        scoreText.text = playerScore.ToString();
        positionText.text = centreArearate.ToString();

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
            string sceneName = SceneManager.GetActiveScene().name;
            sumary.text = "good";
            CalculateStarDisPlay();
            Debug.Log(centreArearate);
            PlayerPrefs.SetInt(sceneName, 1);
        }

    }
    //计算当前位置
    private void CalculatePosition()
    {
        if (!isInEdgeArea)
        {
            centreAreaCount++;
        }
        else
        {
            edgeAreaCount++;
        }
        centreArearate = centreAreaCount * 1.0f / (edgeAreaCount+ centreAreaCount);
    }

    private void UpdatePositionState()
    {
        if (flowerTransform)
        {
            if (flowerTransform.position.magnitude < 1)
            {
                Debug.Log("在边缘");
                isInEdgeArea = false;
            }
            else
            {
                Debug.Log("在中心");
                isInEdgeArea = true;
            }

        }
    }

    private void CalculateStarDisPlay()
    {
        if (centreArearate>0.5)
        {
            positionStar.color = new Color(positionStar.color.r, positionStar.color.g, positionStar.color.b, 0.25f);
        }
        if (hitCount<20)
        {
            rhythmStar.color = new Color(rhythmStar.color.r, rhythmStar.color.g, rhythmStar.color.b, 0.25f);
        }
        if (playerScore > 10000)
        {
            hitStar.color = new Color(hitStar.color.r, hitStar.color.g, hitStar.color.b, 0.25f);
        }
    }

    public bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
