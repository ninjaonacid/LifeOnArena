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
    public class DynamicJoyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

        public void EnableJoystick()
        {
            _joyStickCanvasGroup.interactable = true;
            _joyStickCanvasGroup.blocksRaycasts = true;
            _joyStickCanvasGroup.alpha = 1;
        }

        public void DisableJoystick()
        {
            _joyStickCanvasGroup.interactable = false;
            _joyStickCanvasGroup.blocksRaycasts = false;
            _joyStickCanvasGroup.alpha = 0;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _background.RectTransform.position = eventData.position;
            _joystickCenter = eventData.position;
            _background.Show();
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
            if (_behaviour == JoystickBehaviour.Dynamic)
            {
                _background.Hide();
            }
            
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
