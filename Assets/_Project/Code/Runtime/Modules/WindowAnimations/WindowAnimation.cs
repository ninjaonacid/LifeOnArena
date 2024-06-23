using UnityEngine;

namespace Code.Runtime.Modules.WindowAnimations
{
    public abstract class WindowAnimation : MonoBehaviour
    {
        [SerializeField] protected RectTransform _windowRectTransform;
        [SerializeField] protected CanvasGroup _canvasGroup;

        public abstract void ShowAnimation();
        public abstract void CloseAnimation();
        public abstract void HideAnimation();
    }
}
