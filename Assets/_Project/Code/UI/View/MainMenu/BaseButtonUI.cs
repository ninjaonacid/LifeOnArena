using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.MainMenu
{
    public abstract class BaseButtonUI : MonoBehaviour, IPointerClickHandler
    {
        public abstract void OnPointerClick(PointerEventData eventData);
    }
}
