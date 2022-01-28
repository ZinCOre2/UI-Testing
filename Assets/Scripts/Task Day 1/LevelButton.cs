using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Scripts
{
    public class LevelButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private int level;
        [SerializeField] private Image image;
        
        public GameObject[] fullStars;
        public GameObject[] emptyStars;

        public GameObject lockImage;

        private void Awake()
        {
            //if (LevelSelectHandler.Instance.Levels[level - 1].Locked) { LockLevel(); }
            
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

        public void LockLevel()
        {
            image.color = Color.gray;
            lockImage.SetActive(true);
        }
        
        public void UnlockLevel()
        {
            image.color = Color.white;
            lockImage.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (LevelSelectHandler.Instance.Levels[level - 1].Locked) {return;}
            LevelSelectHandler.Instance.SelectLevel(level - 1);
        }
    }
}