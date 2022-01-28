using System;
using System.Collections;
using System.Collections.Generic;
using Task_Day_2;
using UnityEngine;
using UnityEngine.UI;

public class ResourceWindow : MonoBehaviour
{
    public enum ResourceType
    {
        Stars,
        Knowledge,
        Pots
    };
    
    public event Action<string> OnNameConfirmed;
    public event Action<ResourceType, int> OnResourcesChanged;
    public event Action OnWindowClosed;
    
    [SerializeField] private InputField inputField;
    [SerializeField] private Slider starSlider, knowledgeSlider, potSlider;
    [SerializeField] private Button closeButton;

    private void Start()
    { 
        inputField.onEndEdit.AddListener(ChangeTPointName);
        
        starSlider.onValueChanged.AddListener(ChangeTPointStars);
        knowledgeSlider.onValueChanged.AddListener(ChangeTPointKnowledge);
        potSlider.onValueChanged.AddListener(ChangeTPointPots);
        
        closeButton.onClick.AddListener(CloseWindow);

        OnNameConfirmed += GameController.Instance.OnTPointNameChange;
        OnResourcesChanged += GameController.Instance.OnTPointResourcesChange;
        OnWindowClosed += GameController.Instance.OnResourceWindowClose;
    }

    public void SetWindowData(TPoint point)
    {
        inputField.text = point.PointName;
        
        starSlider.value = point.Stars;
        knowledgeSlider.value = point.Knowledge;
        potSlider.value = point.Pots;
    }
    
    private void ChangeTPointName(string newName)
    {
        OnNameConfirmed?.Invoke(newName);
    }
    private void ChangeTPointStars(float amount)
    {
        OnResourcesChanged?.Invoke(ResourceType.Stars, (int)amount);
    }
    private void ChangeTPointKnowledge(float amount)
    {
        OnResourcesChanged?.Invoke(ResourceType.Knowledge, (int)amount);
    }
    private void ChangeTPointPots(float amount)
    {
        OnResourcesChanged?.Invoke(ResourceType.Pots, (int)amount);
    }

    private void CloseWindow()
    {
        OnWindowClosed?.Invoke();
    }
}
