using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
    public class SimpleInputDragListener : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private int pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
        public ISimpleInputDraggable Listener { get; set; }

        public void OnDrag(PointerEventData eventData)
        {
            if (pointerId != eventData.pointerId)
                return;

            Listener.OnDrag(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Listener.OnPointerDown(eventData);
            pointerId = eventData.pointerId;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (pointerId != eventData.pointerId)
                return;

            Listener.OnPointerUp(eventData);
            pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
        }

        private void Awake()
        {
            var graphic = GetComponent<Graphic>();
            if (!graphic)
                graphic = gameObject.AddComponent<NonDrawingGraphic>();

            graphic.raycastTarget = true;
        }

        public void Stop()
        {
            pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
        }
    }
}