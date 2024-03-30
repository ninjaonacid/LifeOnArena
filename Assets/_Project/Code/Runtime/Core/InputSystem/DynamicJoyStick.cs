using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Runtime.Core.InputSystem
{
    public class DynamicJoyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private CanvasGroup _joyStickCanvas;
        [SerializeField] private RectTransform _stick;
        
        private const string kDynamicOriginClickable = "DynamicOriginClickable";
        
         public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            BeginInteraction(eventData.position, eventData.pressEventCamera);
        }
         
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
                throw new System.ArgumentNullException(nameof(eventData));

            MoveStick(eventData.position, eventData.pressEventCamera);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {

            EndInteraction();
        }

        private void Start()
        {
            m_StartPos = ((RectTransform)transform).anchoredPosition;

            if (m_Behaviour != Behaviour.ExactPositionWithDynamicOrigin) return;
            m_PointerDownPos = m_StartPos;

            var dynamicOrigin = new GameObject(kDynamicOriginClickable, typeof(Image));
            dynamicOrigin.transform.SetParent(transform);
            var image = dynamicOrigin.GetComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
            var rectTransform = (RectTransform)dynamicOrigin.transform;
            rectTransform.sizeDelta = new Vector2(m_DynamicOriginRange * 2, m_DynamicOriginRange * 2);
            rectTransform.localScale = new Vector3(1, 1, 0);
            rectTransform.anchoredPosition3D = Vector3.zero;

            image.sprite = SpriteUtilities.CreateCircleSprite(16, new Color32(255, 255, 255, 255));
            image.alphaHitTestMinimumThreshold = 0.5f;

            HideJoystick();
        }

        private void BeginInteraction(Vector2 pointerPosition, Camera uiCamera)
        {
            var canvasRect = transform.parent?.GetComponentInParent<RectTransform>();
            if (canvasRect == null)
            {
                Debug.LogError("OnScreenStick needs to be attached as a child to a UI Canvas to function properly.");
                return;
            }

            switch (m_Behaviour)
            {
                case Behaviour.RelativePositionWithStaticOrigin:
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointerPosition, uiCamera, out m_PointerDownPos);
                    break;
                case Behaviour.ExactPositionWithStaticOrigin:
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointerPosition, uiCamera, out m_PointerDownPos);
                    MoveStick(pointerPosition, uiCamera);
                    break;
                case Behaviour.ExactPositionWithDynamicOrigin:
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointerPosition, uiCamera, out var pointerDown);
                    m_PointerDownPos = ((RectTransform)transform).anchoredPosition = pointerDown;
                    _stick.anchoredPosition = Vector2.zero;

                    ShowJoystick();
                    
                    break;
            }
        }

        private void MoveStick(Vector2 pointerPosition, Camera uiCamera)
        {
            var canvasRect = transform.parent?.GetComponentInParent<RectTransform>();
            if (canvasRect == null)
            {
                Debug.LogError("OnScreenStick needs to be attached as a child to a UI Canvas to function properly.");
                return;
            }
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointerPosition, uiCamera, out var position);
            var delta = position - m_PointerDownPos;

            switch (m_Behaviour)
            {
                case Behaviour.RelativePositionWithStaticOrigin:
                    delta = Vector2.ClampMagnitude(delta, movementRange);
                    ((RectTransform)transform).anchoredPosition = (Vector2)m_StartPos + delta;
                    break;

                case Behaviour.ExactPositionWithStaticOrigin:
                    delta = position - (Vector2)m_StartPos;
                    delta = Vector2.ClampMagnitude(delta, movementRange);
                    ((RectTransform)transform).anchoredPosition = (Vector2)m_StartPos + delta;
                    break;

                case Behaviour.ExactPositionWithDynamicOrigin:
                    delta = Vector2.ClampMagnitude(delta, movementRange);
                    _stick.anchoredPosition = delta;
                    break;
            }

            var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
            SendValueToControl(newPos);
        }

        private void EndInteraction()
        {
            ((RectTransform)transform).anchoredPosition = m_PointerDownPos = m_StartPos;
            
            HideJoystick();
            
            SendValueToControl(Vector2.zero);
        }

        private void OnPointerDown(InputAction.CallbackContext ctx)
        {
            Debug.Assert(UnityEngine.EventSystems.EventSystem.current != null);

            var screenPosition = Vector2.zero;
            if (ctx.control?.device is Pointer pointer)
                screenPosition = pointer.position.ReadValue();

            m_PointerEventData.position = screenPosition;
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(m_PointerEventData, m_RaycastResults);
            if (m_RaycastResults.Count == 0)
                return;

            var stickSelected = false;
            foreach (var result in m_RaycastResults)
            {
                if (result.gameObject != gameObject) continue;

                stickSelected = true;
                break;
            }

            if (!stickSelected)
                return;

            BeginInteraction(screenPosition, GetCameraFromCanvas());
            m_PointerMoveAction.performed += OnPointerMove;
        }

        private void OnPointerMove(InputAction.CallbackContext ctx)
        {
            // only pointer devices are allowed
            Debug.Assert(ctx.control?.device is Pointer);

            var screenPosition = ((Pointer)ctx.control.device).position.ReadValue();

            MoveStick(screenPosition, GetCameraFromCanvas());
        }

        private void OnPointerUp(InputAction.CallbackContext ctx)
        {
            EndInteraction();
            m_PointerMoveAction.performed -= OnPointerMove;
        }

        private Camera GetCameraFromCanvas()
        {
            var canvas = GetComponentInParent<Canvas>();
            var renderMode = canvas?.renderMode;
            if (renderMode == RenderMode.ScreenSpaceOverlay
                || (renderMode == RenderMode.ScreenSpaceCamera && canvas?.worldCamera == null))
                return null;

            return canvas?.worldCamera ?? Camera.main;
        }

        private void ShowJoystick()
        {
            _joyStickCanvas.alpha = 1f;
        }

        private void HideJoystick()
        {
            _joyStickCanvas.alpha = 0f;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.matrix = ((RectTransform)transform.parent).localToWorldMatrix;

            var startPos = ((RectTransform)transform).anchoredPosition;
            if (Application.isPlaying)
                startPos = m_StartPos;

            Gizmos.color = new Color32(84, 173, 219, 255);

            var center = startPos;
            if (Application.isPlaying && m_Behaviour == Behaviour.ExactPositionWithDynamicOrigin)
                center = m_PointerDownPos;

            DrawGizmoCircle(center, m_MovementRange);

            if (m_Behaviour != Behaviour.ExactPositionWithDynamicOrigin) return;

            Gizmos.color = new Color32(158, 84, 219, 255);
            DrawGizmoCircle(startPos, m_DynamicOriginRange);
        }

        private void DrawGizmoCircle(Vector2 center, float radius)
        {
            for (var i = 0; i < 32; i++)
            {
                var radians = i / 32f * Mathf.PI * 2;
                var nextRadian = (i + 1) / 32f * Mathf.PI * 2;
                Gizmos.DrawLine(
                    new Vector3(center.x + Mathf.Cos(radians) * radius, center.y + Mathf.Sin(radians) * radius, 0),
                    new Vector3(center.x + Mathf.Cos(nextRadian) * radius, center.y + Mathf.Sin(nextRadian) * radius, 0));
            }
        }

        private void UpdateDynamicOriginClickableArea()
        {
            var dynamicOriginTransform = transform.Find(kDynamicOriginClickable);
            if (dynamicOriginTransform)
            {
                var rectTransform = (RectTransform)dynamicOriginTransform;
                rectTransform.sizeDelta = new Vector2(m_DynamicOriginRange * 2, m_DynamicOriginRange * 2);
            }
        }
        
        public float movementRange
        {
            get => m_MovementRange;
            set => m_MovementRange = value;
        }
        
        public float dynamicOriginRange
        {
            get => m_DynamicOriginRange;
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (m_DynamicOriginRange != value)
                {
                    m_DynamicOriginRange = value;
                    UpdateDynamicOriginClickableArea();
                }
            }
        }
        
        [FormerlySerializedAs("movementRange")]
        [SerializeField]
        [Min(0)]
        private float m_MovementRange = 50;

        [SerializeField]
        [Tooltip("Defines the circular region where the onscreen control may have it's origin placed.")]
        [Min(0)]
        private float m_DynamicOriginRange = 100;

        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;

        [SerializeField]
        [Tooltip("Choose how the onscreen stick will move relative to it's origin and the press position.\n\n" +
            "RelativePositionWithStaticOrigin: The control's center of origin is fixed. " +
            "The control will begin un-actuated at it's centered position and then move relative to the pointer or finger motion.\n\n" +
            "ExactPositionWithStaticOrigin: The control's center of origin is fixed. The stick will immediately jump to the " +
            "exact position of the click or touch and begin tracking motion from there.\n\n" +
            "ExactPositionWithDynamicOrigin: The control's center of origin is determined by the initial press position. " +
            "The stick will begin un-actuated at this center position and then track the current pointer or finger position.")]
        private Behaviour m_Behaviour;

        [SerializeField]
        [Tooltip("The action that will be used to detect pointer down events on the stick control. Note that if no bindings " +
            "are set, default ones will be provided.")]
        private InputAction m_PointerDownAction;

        [SerializeField]
        [Tooltip("The action that will be used to detect pointer movement on the stick control. Note that if no bindings " +
            "are set, default ones will be provided.")]
        private InputAction m_PointerMoveAction;

        private Vector3 m_StartPos;
        private Vector2 m_PointerDownPos;

        [NonSerialized]
        private List<RaycastResult> m_RaycastResults;
        [NonSerialized]
        private PointerEventData m_PointerEventData;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }

        /// <summary>Defines how the onscreen stick will move relative to it's origin and the press position.</summary>
        public Behaviour behaviour
        {
            get => m_Behaviour;
            set => m_Behaviour = value;
        }

        /// <summary>Defines how the onscreen stick will move relative to it's center of origin and the press position.</summary>
        public enum Behaviour
        {
            /// <summary>The control's center of origin is fixed in the scene.
            /// The control will begin un-actuated at it's centered position and then move relative to the press motion.</summary>
            RelativePositionWithStaticOrigin,

            /// <summary>The control's center of origin is fixed in the scene.
            /// The control may begin from an actuated position to ensure it is always tracking the current press position.</summary>
            ExactPositionWithStaticOrigin,

            /// <summary>The control's center of origin is determined by the initial press position.
            /// The control will begin unactuated at this center position and then track the current press position.</summary>
            ExactPositionWithDynamicOrigin
        }
    }

    
}
