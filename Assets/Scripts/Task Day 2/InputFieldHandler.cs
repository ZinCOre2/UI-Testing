using System;
using UnityEngine;
using UnityEngine.UI;

namespace Task_Day_2
{
    public class InputFieldHandler : MonoBehaviour
    {
        public event Action<string> OnNameConfirmed;
        
        [SerializeField] private InputField inputField;

        private void Start()
        {
            inputField.onEndEdit.AddListener(ChangeTPointName);
        }

        private void ChangeTPointName(string newName)
        {
            OnNameConfirmed?.Invoke(newName);
        }
    }
}