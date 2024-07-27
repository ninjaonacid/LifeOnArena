using Code.Runtime.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Code.Runtime.Core.InputSystem
{
    public enum JoystickBehaviour
    {
        Dynamic,
        Static
    }
    public class DynamicJoyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CanvasGroup _joyStickCanvasGroup;
        [SerializeField] private CanvasElement _background;
        [SerializeField] private CanvasElement _stick;
        [SerializeField] private float movementRange = 50f;
        [SerializeField] private JoystickBehaviour _behaviour;

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
            
            if (_behaviour == JoystickBehaviour.Static)
            {
                _background.Show();
            }
        }

        public void Show(bool value)
        {
            _joyStickCanvasGroup.alpha = value ? 1 : 0;
            _joyStickCanvasGroup.interactable = value;
            _joyStickCanvasGroup.blocksRaycasts = value;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_background.RectTransform, eventData.position))
            {
                OnDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - _joystickCenter;
            direction = Vector2.ClampMagnitude(direction, movementRange);
            _input = direction / movementRange;
            _stick.RectTransform.position = _joystickCenter + direction;
            SendValueToControl(_input);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetStick();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _input = Vector2.zero;
            SendValueToControl(_input);
        }

        private void ResetStick()
        {
            _input = Vector2.zero;
            _stick.RectTransform.position = _joystickCenter;
            
            if (_behaviour == JoystickBehaviour.Dynamic)
            {
                _background.Hide();
            }
            SendValueToControl(_input);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetStick();
        }
    }
}
