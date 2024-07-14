using System;
using UnityEngine;

namespace Code.Runtime.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasElement : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
        }

        public void Show(bool value)
        {
            _canvasGroup.alpha = value ? 1 : 0;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
