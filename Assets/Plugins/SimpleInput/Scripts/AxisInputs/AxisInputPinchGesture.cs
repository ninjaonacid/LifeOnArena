using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleInputNamespace
{
    [RequireComponent(typeof(SimpleInputMultiDragListener))]
    public class AxisInputPinchGesture : MonoBehaviour, ISimpleInputDraggableMultiTouch
    {
        public SimpleInput.AxisInput axis = new SimpleInput.AxisInput();

        private SimpleInputMultiDragListener eventReceiver;
        public bool invertValue;

        public float sensitivity = 1f;

        public int Priority => 2;

        public bool OnUpdate(List<PointerEventData> mousePointers, List<PointerEventData> touchPointers,
            ISimpleInputDraggableMultiTouch activeListener)
        {
            axis.value = 0f;

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

            var pinchAmount = (touch2.delta - touch1.delta).magnitude;
            var zoomingOut = (touch2.position - touch1.position).sqrMagnitude <
                             (touch2.position - touch2.delta - (touch1.position - touch1.delta)).sqrMagnitude;
            if (invertValue != zoomingOut)
                pinchAmount = -pinchAmount;

            axis.value = pinchAmount * sensitivity * SimpleInputUtils.ResolutionMultiplier;
            return true;
        }

        private void Awake()
        {
            eventReceiver = GetComponent<SimpleInputMultiDragListener>();
        }

        private void OnEnable()
        {
            eventReceiver.AddListener(this);
            axis.StartTracking();
        }

        private void OnDisable()
        {
            eventReceiver.RemoveListener(this);
            axis.StopTracking();
        }
    }
}