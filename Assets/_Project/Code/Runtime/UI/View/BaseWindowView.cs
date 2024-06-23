using System;
using Code.Runtime.Modules.WindowAnimations;
using Code.Runtime.UI.Buttons;
using UnityEngine;

namespace Code.Runtime.UI.View
{
    public abstract class BaseWindowView : MonoBehaviour, IDisposable
    {
        public ScreenID ScreenId;
        public CloseButton CloseButton;

        [SerializeField] private WindowAnimation _animation;
        public virtual void Show()
        {
            gameObject.SetActive(true);
            
            if (_animation != null)
            {
                _animation.ShowAnimation();
            }
           
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Close()
        {
            Debug.Log("Close Called");
            
            if (_animation != null)
            {
                _animation.CloseAnimation();
            }
            
            Destroy(gameObject);
        }

        public void Dispose()
        {
        }
    }
}
