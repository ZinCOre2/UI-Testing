using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(TestInside);
    }

    public void Test()
    {
        Debug.Log("Button pressed!");
    }

    private void TestInside()
    {
        Debug.Log("Button pressed inside!");
        button.onClick.RemoveAllListeners();
    }
}
