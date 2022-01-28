using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Task_Day_2
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        public List<TPoint> TradingPoints;
        public TPoint SelectedPoint { get; private set; }

        [SerializeField] private ResourceWindow resourceWindow;
        [SerializeField] private GameObject mapMarker;

        [SerializeField] private float selectedPointScale = 1.5f;
        
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
            resourceWindow.gameObject.SetActive(false);
            mapMarker.SetActive(false);
        }

        private void Update()
        {
            if (SelectedPoint == null) { Debug.Log("No point selected"); return; }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectedPoint.Coins = 1;
                SelectedPoint.SetCoins();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectedPoint.Coins = 2;
                SelectedPoint.SetCoins();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectedPoint.Coins = 3;
                SelectedPoint.SetCoins();
            }
        }

        public void OnTPointSelected(TPoint point)
        {
            SelectedPoint?.ScalePoint(selectedPointScale, 1f);
            
            SelectedPoint = point;
            SelectedPoint.ScalePoint(1f, selectedPointScale);

            resourceWindow.gameObject.SetActive(true);
            resourceWindow.SetWindowData(point);
            
            mapMarker.SetActive(true);
            mapMarker.transform.position = point.transform.position;
        }
        public void OnTPointNameChange(string str)
        {
            SelectedPoint.PointName = str;
        }
        public void OnTPointResourcesChange(ResourceWindow.ResourceType rt, int amount)
        {
            switch (rt)
            {
                case ResourceWindow.ResourceType.Stars:
                    SelectedPoint.Stars = amount;
                    break;
                case ResourceWindow.ResourceType.Knowledge:
                    SelectedPoint.Knowledge = amount;
                    break;
                case ResourceWindow.ResourceType.Pots:
                    SelectedPoint.Pots = amount;
                    break;
            }
        }
        
        public void OnResourceWindowClose()
        {
            resourceWindow.gameObject.SetActive(false);
            mapMarker.SetActive(false);

            SelectedPoint.ScalePoint(selectedPointScale, 1f);
            SelectedPoint = null;
        }
    }
}