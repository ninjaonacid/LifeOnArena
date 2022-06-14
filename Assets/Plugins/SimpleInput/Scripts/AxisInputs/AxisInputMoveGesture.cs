using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleInputNamespace
{
    [RequireComponent(typeof(SimpleInputMultiDragListener))]
    public class AxisInputMoveGesture : MonoBehaviour, ISimpleInputDraggableMultiTouch
    {
        private SimpleInputMultiDragListener eventReceiver;
        public SimpleInput.AxisInput horizontal = new SimpleInput.AxisInput("Mouse X");
        public bool invertValue = true;

        public float sensitivity = 1f;
        public SimpleInput.AxisInput vertical = new SimpleInput.AxisInput("Mouse Y");

        public int Priority => 2;

        public bool OnUpdate(List<PointerEventData> mousePointers, List<PointerEventData> touchPointers,
            ISimpleInputDraggableMultiTouch activeListener)
        {
            horizontal.value = 0f;
            vertical.value = 0f;

            if (activeListener != null && activeListener.Priority > Priority)
                return false;

            if (touchPointers.Count < 2)
            {
                if (ReferenceEquals(activeListener, this) && touchPointers.Count == 1)
                    touchPointers[0].pressPosition = touchPointers[0].position;

                return false;
            }

            var touch1 = touchPointers[touchPointers.Count - 1];
            var touch2 = touchPointers[touchPointers.Count - 2];

            var pinchAmount = sensitivity * SimpleInputUtils.ResolutionMultiplier * (touch1.delta + touch2.delta);
            if (invertValue)
                pinchAmount = -pinchAmount;

            horizontal.value = pinchAmount.x;
            vertical.value = pinchAmount.y;

            return true;
        }

        private void Awake()
        {
            eventReceiver = GetComponent<SimpleInputMultiDragListener>();
        }

        private void OnEnable()
        {
            eventReceiver.AddListener(this);

            horizontal.StartTracking();
            vertical.StartTracking();
        }

        private void OnDisable()
        {
            eventReceiver.RemoveListener(this);

            horizontal.StopTracking();
            vertical.StopTracking();
        }
    }
}