using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
    public class AxisInputUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public SimpleInput.AxisInput axis = new SimpleInput.AxisInput();
        public float value = 1f;

        public void OnPointerDown(PointerEventData eventData)
        {
            axis.value = value;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            axis.value = 0f;
        }

        private void Awake()
        {
            var graphic = GetComponent<Graphic>();
            if (graphic != null)
                graphic.raycastTarget = true;
        }

        private void OnEnable()
        {
            axis.StartTracking();
        }

        private void OnDisable()
        {
            axis.StopTracking();
        }
    }
}