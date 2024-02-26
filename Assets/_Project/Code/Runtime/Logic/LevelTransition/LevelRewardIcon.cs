using UnityEngine;

namespace Code.Runtime.Logic.LevelTransition
{
    public class LevelRewardIcon : MonoBehaviour
    {
       [SerializeField] private  SpriteRenderer _spriteRenderer;

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
