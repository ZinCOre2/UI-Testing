using System;
using System.Collections;
using System.Collections.Generic;
using Scripts;
using UnityEngine;

public class LevelSelectHandler : MonoBehaviour
{
    public struct LevelData
    {
        public bool Locked { get; private set; }
        public int Stars { get; private set; }

        public LevelData(int err = 0)
        {
            Locked = true;
            Stars = 0;
        }

        public void UnlockLevel()
        {
            Locked = false;
        }
        
        public void FinishLevel(int stars)
        {
            Stars = stars > Stars ? stars : Stars;
        }
    }

    public static LevelSelectHandler Instance;
    
    [SerializeField] private GameObject levelSelectScreen;
    [SerializeField] private GameObject victoryPopup;
    [SerializeField] private LevelButton[] levelButtons;
    
    [Header("Victory Screen")]
    [SerializeField] private GameObject[] emptyStars;
    [SerializeField] private GameObject[] fullStars;
        
    [SerializeField] private float initAnimScale = 2.5f;
    [SerializeField] private float scalingAnimTime = 0.7f;
    
    public LevelData[] Levels = new LevelData[24];
    
    private int _selectedLevel = 0;
    private bool _onLevel = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < fullStars.Length; i++)
        {
            fullStars[i].SetActive(false);
        }

        SelectLevel(1);
    }

    private void Update()
    {
        if (_onLevel) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FinishLevel(1);
                Debug.Log("1");
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                FinishLevel(2);
                Debug.Log("2");
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FinishLevel(3);
                Debug.Log("3");
            }
        }
    }

    public void SelectLevel(int level)
    {
        _selectedLevel = level;
        _onLevel = true;
        
        levelSelectScreen.SetActive(false);
        victoryPopup.SetActive(false);
    }
    public void FinishLevel(int stars)
    {
        Levels[_selectedLevel].FinishLevel(stars);
        levelButtons[_selectedLevel - 1].SetStars(Levels[_selectedLevel].Stars);

        _onLevel = false;
        
        VictoryScreen(stars);
    }

    public void VictoryScreen(int starCount)
    {
        victoryPopup.SetActive(true);
        
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

            emptyStars[i].SetActive(false);
            StartCoroutine(TweenScale(fullStars[i].transform, initAnimScale, 1f, scalingAnimTime));
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
            Debug.Log(scaledObject.localScale);
        }
        
        scaledObject.localScale = Vector3.one * resultScale;
    }

    public void BackToSelectLevelScreen()
    {
        StartCoroutine(FadeOut(victoryPopup.GetComponent<CanvasGroup>(), 1f, 1f, levelSelectScreen));
    }
    
    IEnumerator FadeOut(CanvasGroup fadingWindow, float initAlpha, float time, GameObject newWindow = null)
    {
        float t = 0f;
        time /= 2;

        fadingWindow.alpha = initAlpha;

        while (t < time)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            t += Time.deltaTime;
            fadingWindow.alpha = 1 - ((t - time) / time);
        }
        
        fadingWindow.alpha = 1f;
        fadingWindow.gameObject.SetActive(false);

        yield return new WaitForSeconds(time);
        
        if (newWindow != null)
        {
            ShowUpWindow(newWindow);
        }
    }
    

    private void ShowUpWindow(GameObject scaledObject)
    {
        scaledObject.SetActive(true);
        StartCoroutine(TweenScale(scaledObject.transform, 0.1f, 1f, 1f));
    }
}
