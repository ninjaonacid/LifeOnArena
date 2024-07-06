using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    [RequireComponent(typeof(Image), typeof(CanvasGroup))]
    public class ImageUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        public void Show(bool value)
        {
            _canvasGroup.alpha = value ? 1 : 0;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}
