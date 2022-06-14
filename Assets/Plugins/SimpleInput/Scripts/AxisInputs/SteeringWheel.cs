using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
    public class SteeringWheel : MonoBehaviour, ISimpleInputDraggable
    {
        public SimpleInput.AxisInput axis = new SimpleInput.AxisInput("Horizontal");
        private Vector2 centerPoint;

        public float maximumSteeringAngle = 200f;
        public float valueMultiplier = 1f;

        private Graphic wheel;

        private bool wheelBeingHeld;
        private float wheelPrevAngle;
        public float wheelReleasedSpeed = 350f;

        private RectTransform wheelTR;
        public float Value { get; private set; }

        public float Angle { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Executed when mouse/finger starts touching the steering wheel
            wheelBeingHeld = true;
            centerPoint = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, wheelTR.position);
            wheelPrevAngle = Vector2.Angle(Vector2.up, eventData.position - centerPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Executed when mouse/finger is dragged over the steering wheel
            var pointerPos = eventData.position;

            var wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);

            // Do nothing if the pointer is too close to the center of the wheel
            if ((pointerPos - centerPoint).sqrMagnitude >= 400f)
            {
                if (pointerPos.x > centerPoint.x)
                    Angle += wheelNewAngle - wheelPrevAngle;
                else
                    Angle -= wheelNewAngle - wheelPrevAngle;
            }

            // Make sure wheel angle never exceeds maximumSteeringAngle
            Angle = Mathf.Clamp(Angle, -maximumSteeringAngle, maximumSteeringAngle);
            wheelPrevAngle = wheelNewAngle;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Executed when mouse/finger stops touching the steering wheel
            // Performs one last OnDrag calculation, just in case
            OnDrag(eventData);

            wheelBeingHeld = false;
        }

        private void Awake()
        {
            wheel = GetComponent<Graphic>();
            wheelTR = wheel.rectTransform;

            var eventReceiver = gameObject.AddComponent<SimpleInputDragListener>();
            eventReceiver.Listener = this;
        }

        private void OnEnable()
        {
            axis.StartTracking();
            SimpleInput.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            wheelBeingHeld = false;
            Angle = wheelPrevAngle = Value = 0f;
            wheelTR.localEulerAngles = Vector3.zero;

            axis.StopTracking();
            SimpleInput.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            // If the wheel is released, reset the rotation
            // to initial (zero) rotation by wheelReleasedSpeed degrees per second
            if (!wheelBeingHeld && Angle != 0f)
            {
                var deltaAngle = wheelReleasedSpeed * Time.deltaTime;
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(Angle))
                    Angle = 0f;
                else if (Angle > 0f)
                    Angle -= deltaAngle;
                else
                    Angle += deltaAngle;
            }

            // Rotate the wheel image
            wheelTR.localEulerAngles = new Vector3(0f, 0f, -Angle);

            Value = Angle * valueMultiplier / maximumSteeringAngle;
            axis.value = Value;
        }
    }
}