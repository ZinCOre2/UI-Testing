using System;
using System.Collections;
using System.Collections.Generic;
using Task_Day_2;
using UnityEngine;
using UnityEngine.UI;

public class TPointButton : MonoBehaviour
{
    [SerializeField] private TPoint point;
    
    public event Action<TPoint> OnTPointSelected;

    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(LoadTPointData);
        
        OnTPointSelected += GameController.Instance.OnTPointSelected;
    }

    private void LoadTPointData()
    {
        OnTPointSelected?.Invoke(point);
    }
}
