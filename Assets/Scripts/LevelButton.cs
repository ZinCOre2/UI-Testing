using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts
{
    public class LevelButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int level;
        
        public GameObject[] fullStars;
        public GameObject[] emptyStars;

        private void Start()
        {
            for (int i = 0; i < fullStars.Length; i++)
            {
                fullStars[i].SetActive(false);
            }
        }

        public void SetStars(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                fullStars[i].SetActive(true);
                emptyStars[i].SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LevelSelectHandler.Instance.SelectLevel(level);
        }
    }
}