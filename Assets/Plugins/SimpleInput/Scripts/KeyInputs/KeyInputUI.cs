using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
    public class KeyInputUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public SimpleInput.KeyInput key = new SimpleInput.KeyInput();

        public void OnPointerDown(PointerEventData eventData)
        {
            key.value = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            key.value = false;
        }

        private void Awake()
        {
            var graphic = GetComponent<Graphic>();
            if (graphic != null)
                graphic.raycastTarget = true;
        }

        private void OnEnable()
        {
            key.StartTracking();
        }

        private void OnDisable()
        {
            key.StopTracking();
        }
    }
}