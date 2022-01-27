using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot;
    [SerializeField] private GameObject[] emptyStars;
    [SerializeField] private GameObject[] fullStars;
    [SerializeField] private Transform[] fullStarPositions;

    [SerializeField] private float initAnimScale = 2.5f;
    [SerializeField] private float starDelayTime = 0.5f;
    [SerializeField] private float scalingAnimTime = 0.7f;

    [SerializeField] private float flightTime = 1f;

    void Start()
    {
        for (int i = 0; i < fullStars.Length; i++) 
        {
            fullStars[i].SetActive(false);
        }
        
        panelRoot.SetActive(false);

        scalingAnimTime /= 2;
        flightTime /= 2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Victory(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Victory(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Victory(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Victory(3);
        }
    }
    
    public void Victory(int starCount)
    {
        panelRoot.SetActive(true);

        for (int i = 0; i < emptyStars.Length; i++)
        {
            fullStars[i].SetActive(false);
            emptyStars[i].SetActive(true);
        }
        
        StartCoroutine(ShowStarsWithDelay(starCount));
    }

    IEnumerator ShowStarsWithDelay(int starCount)
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        for (int i = 0; i < starCount; i++)
        {
            yield return delay;
            fullStars[i].SetActive(true);
            
            //StartCoroutine(ShootingStar(i));
            
            emptyStars[i].SetActive(false);
            StartCoroutine(TweenScale(fullStars[i].transform));
        }
    }
    
    IEnumerator ShootingStar(int id)
    {
        float t = 0f;
        
        while (t < flightTime)
        {
            t += Time.deltaTime;
            fullStars[id].transform.position = Vector3.Lerp(fullStarPositions[id].transform.position,
                emptyStars[id].transform.position, t / flightTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        fullStars[id].transform.position = emptyStars[id].transform.position;
        emptyStars[id].SetActive(false);
    }
    IEnumerator TweenScale(Transform scaledObject)
    {
        float t = 0f;
        
        scaledObject.localScale = Vector3.one * initAnimScale;

        while (t < scalingAnimTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            t += Time.deltaTime;
            scaledObject.localScale = Vector3.one + Vector3.one * (initAnimScale - 1) * (scalingAnimTime - t);
        }
        
        scaledObject.localScale = Vector3.one;
    }
}
