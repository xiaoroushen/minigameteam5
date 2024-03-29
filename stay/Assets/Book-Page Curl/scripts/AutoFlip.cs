﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Book))]
public class AutoFlip : MonoBehaviour {
    public FlipMode Mode;
    public float PageFlipTime = 1;
    public float TimeBetweenPages = 1;
    public float DelayBeforeStarting = 0;
    public bool AutoStartFlip=true;
    public Book ControledBook;
    public int AnimationFramesCount = 40;
    public AudioSource myAudioSource;
    public GameObject levelEnter;

    public GameObject chooseEnter1;
    public GameObject chooseEnter2;

    bool isFlipping = false;

    // Use this for initialization
    void Start () {
        if (!ControledBook)
            ControledBook = GetComponent<Book>();
        if (AutoStartFlip)
            StartFlipping();
        ControledBook.OnFlip.AddListener(new UnityEngine.Events.UnityAction(PageFlipped));
	}
    void PageFlipped()
    {
        isFlipping = false;
    }
	public void StartFlipping()
    {
        StartCoroutine(FlipToEnd());
    }
    public void FlipRightPage()
    {
        if (isFlipping) return;
        if (ControledBook.currentPage >= ControledBook.TotalPageCount) return;
        isFlipping = true;
        if (ControledBook.currentPage == 8)
        {
            chooseEnter1.SetActive(false);
            chooseEnter2.SetActive(false);
        }
        CancelInvoke("SetEntryActive");
        CancelInvoke("SetLevel4");
        levelEnter.SetActive(false);
        myAudioSource.Play();


        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl)*2 / AnimationFramesCount;
        StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));

        if(ControledBook.currentPage == 6)
        {
            Invoke("SetLevel4", 1.7f);
        }


        if(ControledBook.currentPage != 6)
        {
            if(ControledBook.currentPage != 8)
            {
                Invoke("SetEntryActive", 1.7f);
            }
        }
    }
    public void FlipLeftPage()
    {
        if (isFlipping) return;
        if (ControledBook.currentPage <= 0) return;
        isFlipping = true;
        CancelInvoke("SetEntryActive");
        CancelInvoke("SetLevel4");
        if (ControledBook.currentPage == 8)
        {
            chooseEnter1.SetActive(false);
            chooseEnter2.SetActive(false);
        }
        levelEnter.SetActive(false);
        myAudioSource.Play();
        if (ControledBook.currentPage == 2)
        {
                ControledBook.leftPage.SetActive(false);
        }
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2) * 0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y) * 0.9f;
        float dx = (xl) * 2 / AnimationFramesCount;
        StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));

        if (ControledBook.currentPage == 10)
        {
            Invoke("SetLevel4", 1.7f);
        }
        if (ControledBook.currentPage != 2)
        {   
            if (ControledBook.currentPage != 10)
            {
                Invoke("SetEntryActive", 1.7f);
            }

        }
    }
    IEnumerator FlipToEnd()
    {
        yield return new WaitForSeconds(DelayBeforeStarting);
        float frameTime = PageFlipTime / AnimationFramesCount;
        float xc = (ControledBook.EndBottomRight.x + ControledBook.EndBottomLeft.x) / 2;
        float xl = ((ControledBook.EndBottomRight.x - ControledBook.EndBottomLeft.x) / 2)*0.9f;
        //float h =  ControledBook.Height * 0.5f;
        float h = Mathf.Abs(ControledBook.EndBottomRight.y)*0.9f;
        //y=-(h/(xl)^2)*(x-xc)^2          
        //               y         
        //               |          
        //               |          
        //               |          
        //_______________|_________________x         
        //              o|o             |
        //           o   |   o          |
        //         o     |     o        | h
        //        o      |      o       |
        //       o------xc-------o      -
        //               |<--xl-->
        //               |
        //               |
        float dx = (xl)*2 / AnimationFramesCount;
        switch (Mode)
        {
            case FlipMode.RightToLeft:
                while (ControledBook.currentPage < ControledBook.TotalPageCount)
                {
                    StartCoroutine(FlipRTL(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
            case FlipMode.LeftToRight:
                while (ControledBook.currentPage > 0)
                {
                    StartCoroutine(FlipLTR(xc, xl, h, frameTime, dx));
                    yield return new WaitForSeconds(TimeBetweenPages);
                }
                break;
        }
    }
    IEnumerator FlipRTL(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc + xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);

        ControledBook.DragRightPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookRTLToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x -= dx;
        }
        ControledBook.ReleasePage();
    }
    IEnumerator FlipLTR(float xc, float xl, float h, float frameTime, float dx)
    {
        float x = xc - xl;
        float y = (-h / (xl * xl)) * (x - xc) * (x - xc);
        ControledBook.DragLeftPageToPoint(new Vector3(x, y, 0));
        for (int i = 0; i < AnimationFramesCount; i++)
        {
            y = (-h / (xl * xl)) * (x - xc) * (x - xc);
            ControledBook.UpdateBookLTRToPoint(new Vector3(x, y, 0));
            yield return new WaitForSeconds(frameTime);
            x += dx;
        }
        ControledBook.ReleasePage();
    }

    private void SetEntryActive()
    {
        levelEnter.SetActive(true);
    }

    private void SetLevel4()
    {
        Debug.Log("123");
        chooseEnter1.SetActive(true);
        chooseEnter2.SetActive(true);
    }
}
