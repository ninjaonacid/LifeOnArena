using System;
using Code.Runtime.UI.Buttons;
using UnityEngine;

namespace Code.Runtime.UI.View
{
    public abstract class BaseView : MonoBehaviour, IDisposable
    {
        public ScreenID ScreenId;
        public CloseButton CloseButton;
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            Debug.Log("Close Called");
            Destroy(gameObject);
        }

        public void Dispose()
        {
        }
    }
}
