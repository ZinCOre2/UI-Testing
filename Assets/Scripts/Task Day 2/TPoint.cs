using System;
using System.Collections;
using System.Collections.Generic;
using Task_Day_2;
using UnityEngine;
using UnityEngine.UI;

public class TPoint : MonoBehaviour
{
    //[SerializeField] private string pointName;
    public string PointName;

    //[SerializeField] private int coins;
    [Range(0, 3)] public int Coins;
    [SerializeField] private GameObject[] coinImages;

    //[SerializeField] private int stars;
    public int Stars;
    //[SerializeField] private int knowledge;
    public int Knowledge;
    //[SerializeField] private int pots;
    public int Pots;

    private void Start()
    {
        GameController.Instance.TradingPoints.Add(this);

        for (int i = Coins; i < 3; i++)
        {
            coinImages[i].SetActive(false);
        }
    }

    public void ScalePoint(float initScale, float resultScale)
    {
        StartCoroutine(TweenScale(transform, initScale, resultScale, 0.5f));
    }
    
    public void SetCoins()
    {
        for (int i = 0; i < 3; i++)
        {
            coinImages[i].SetActive(false);
        }
        StartCoroutine(ShowCoinsWithDelay(Coins));
    }
    
    IEnumerator ShowCoinsWithDelay(int coinCount)
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        for (int i = 0; i < Coins; i++)
        {
            coinImages[i].SetActive(true);
            StartCoroutine(TweenScale(coinImages[i].transform, 0.1f, 1f, 1f));
            
            yield return delay;
        }
    }
    IEnumerator TweenScale(Transform scaledObject, float initScale, float resultScale, float time)
    {
        float t = 0f;
        time /= 2;
        
        scaledObject.localScale = Vector3.one * initScale;

        while (t < time)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            t += Time.deltaTime;
            scaledObject.localScale = Vector3.one * resultScale + Vector3.one * (initScale - resultScale) * (time - t);
        }
        
        scaledObject.localScale = Vector3.one * resultScale;
    }
}
