using Code.Runtime.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace _Project.Code.Runtime.Core.InputSystem
{
    public class DynamicJoyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private CanvasElement _background;
        [SerializeField] private CanvasElement _stick;
        [SerializeField] private float movementRange = 50f;

        [InputControl(layout = "Vector2")] [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }

        private Vector2 _joystickCenter;
        private Vector2 _input;

        protected override void OnEnable()
        {
            base.OnEnable();
            _joystickCenter = _background.RectTransform.position;
            _background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _background.RectTransform.position = eventData.position;
            _joystickCenter = eventData.position;
            _background.gameObject.SetActive(true);
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - _joystickCenter;
            _input = (direction.magnitude > movementRange) ? direction.normalized : direction / movementRange;
            _stick.RectTransform.position = _joystickCenter + (_input * movementRange);
            SendValueToControl(_input);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _input = Vector2.zero;
            _stick.RectTransform.position = _joystickCenter;
            _background.gameObject.SetActive(false);
            SendValueToControl(_input);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _input = Vector2.zero;
            SendValueToControl(_input);
        }

    }
}
