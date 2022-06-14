using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RectTransform))]
    public class SimpleInputMultiDragListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private const float POINTERS_VALIDATION_INTERVAL = 5f;
        private ISimpleInputDraggableMultiTouch activeListener;

        private readonly List<ISimpleInputDraggableMultiTouch> listeners = new List<ISimpleInputDraggableMultiTouch>(1);

        private readonly List<PointerEventData> mousePointers = new List<PointerEventData>();

        private float pointersNextValidation = POINTERS_VALIDATION_INTERVAL;
        private List<PointerEventData> touchPointers = new List<PointerEventData>();
        private List<PointerEventData> validPointers = new List<PointerEventData>();

        public void OnPointerDown(PointerEventData eventData)
        {
            var pointers = eventData.IsTouchInput() ? touchPointers : mousePointers;

            for (var i = 0; i < pointers.Count; i++)
                if (pointers[i].pointerId == eventData.pointerId)
                {
                    pointers[i] = eventData;
                    return;
                }

            pointers.Add(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            for (var i = 0; i < mousePointers.Count; i++)
                if (mousePointers[i].pointerId == eventData.pointerId)
                {
                    mousePointers.RemoveAt(i);
                    break;
                }

            for (var i = 0; i < touchPointers.Count; i++)
                if (touchPointers[i].pointerId == eventData.pointerId)
                {
                    touchPointers.RemoveAt(i);
                    break;
                }
        }

        private void Awake()
        {
            var graphic = GetComponent<Graphic>();
            if (!graphic)
                graphic = gameObject.AddComponent<NonDrawingGraphic>();

            graphic.raycastTarget = true;
        }

        private void OnEnable()
        {
            SimpleInput.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            SimpleInput.OnUpdate -= OnUpdate;
        }

        public void AddListener(ISimpleInputDraggableMultiTouch listener)
        {
            var priority = listener.Priority;
            var i = 0;
            while (i < listeners.Count && listeners[i].Priority < priority)
                i++;

            listeners.Insert(i, listener);
        }

        public void RemoveListener(ISimpleInputDraggableMultiTouch listener)
        {
            listeners.Remove(listener);

            if (activeListener == listener)
                activeListener = null;
        }

        private void OnUpdate()
        {
            pointersNextValidation -= Time.unscaledDeltaTime;
            if (pointersNextValidation <= 0f)
            {
                pointersNextValidation = POINTERS_VALIDATION_INTERVAL;
                ValidatePointers();
            }

            for (var i = listeners.Count - 1; i >= 0; i--)
                if (listeners[i].OnUpdate(mousePointers, touchPointers, activeListener))
                {
                    if (activeListener == null || activeListener.Priority < listeners[i].Priority)
                        activeListener = listeners[i];
                }
                else if (activeListener == listeners[i])
                {
                    activeListener = null;
                }

            for (var i = 0; i < mousePointers.Count; i++)
                mousePointers[i].delta = new Vector2(0f, 0f);

            for (var i = 0; i < touchPointers.Count; i++)
                touchPointers[i].delta = new Vector2(0f, 0f);
        }

        private void ValidatePointers()
        {
            for (var i = mousePointers.Count - 1; i >= 0; i--)
                if (!Input.GetMouseButton((int)mousePointers[i].button))
                    mousePointers.RemoveAt(i);

            for (var i = Input.touchCount - 1; i >= 0; i--)
            {
                var fingerId = Input.GetTouch(i).fingerId;
                for (var j = 0; j < touchPointers.Count; j++)
                    if (touchPointers[j].pointerId == fingerId)
                    {
                        validPointers.Add(touchPointers[j]);
                        break;
                    }
            }

            var temp = touchPointers;
            touchPointers = validPointers;
            validPointers = temp;
            validPointers.Clear();
        }
    }
}