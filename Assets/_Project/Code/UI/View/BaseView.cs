using System;
using Code.UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.View
{
    public abstract class BaseView : MonoBehaviour, IDisposable
    {
        public ScreenID ScreenId;
        public Button CloseButton;
        public CloseButton TestCloseButton;
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
