using System;
using UnityEngine;

namespace Code.Logic.LevelTransition
{
    public class LevelRewardIcon : MonoBehaviour
    {
       [SerializeField] private  SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            
        }

        private void Update()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }

        public void HideSprite()
        {
            _spriteRenderer.enabled = false;
        }

        public void ShowSprite()
        {
            _spriteRenderer.enabled = true;
        }
    }
}
