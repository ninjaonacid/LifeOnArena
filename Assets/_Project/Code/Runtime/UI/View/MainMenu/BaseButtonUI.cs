using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.UI.View.MainMenu
{
    public abstract class BaseButtonUI : MonoBehaviour, IPointerClickHandler
    {
        public abstract void OnPointerClick(PointerEventData eventData);
    }
}
