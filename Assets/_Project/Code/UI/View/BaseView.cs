using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View
{
    public abstract class BaseView : MonoBehaviour, IScreenView, IDisposable
    {
        public abstract Type ModelType { get; }
        
        public Button CloseButton;
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }

        public void Dispose()
        {
        }
    }
}
