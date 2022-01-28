using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class InputFieldListener : MonoBehaviour
    {
        [SerializeField] private InputField inputField;

        public void TestOnValueChanged()
        {
            print($"From changed event {inputField.text}");
        }

        public void TestOnEndEdit()
        {
            print($"From end event {inputField.text}");
        }

        private void Start()
        {
            inputField.onValueChanged.AddListener(delegate(string str)
            {
                print($"From CODE!!! changed event {str}");
            });
            
            inputField.onEndEdit.AddListener(delegate(string str)
            {
                print($"From CODE!!! end event {str}");
            });
        }
    }
}