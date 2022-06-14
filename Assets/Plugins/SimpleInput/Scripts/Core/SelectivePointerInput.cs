using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleInputNamespace
{
    public abstract class SelectivePointerInput : MonoBehaviour
    {
        [Tooltip("Valid mouse buttons that can register input through this touchpad")]
        public List<PointerEventData.InputButton> allowedMouseButtons = new List<PointerEventData.InputButton>
        {
            PointerEventData.InputButton.Left, PointerEventData.InputButton.Right, PointerEventData.InputButton.Middle
        };

        [Tooltip("Should touchpad allow touch inputs on touchscreens, or mouse input only")]
        public bool allowTouchInput = true;

        protected PointerEventData GetSatisfyingPointer(List<PointerEventData> mousePointers,
            List<PointerEventData> touchPointers)
        {
            if (allowTouchInput && touchPointers.Count > 0)
                return touchPointers[touchPointers.Count - 1];

            if (mousePointers.Count > 0)
                for (var i = mousePointers.Count - 1; i >= 0; i--)
                {
                    var mouseButton = mousePointers[i].button;
                    for (var j = 0; j < allowedMouseButtons.Count; j++)
                        if (allowedMouseButtons[j] == mouseButton)
                            return mousePointers[i];
                }

            return null;
        }
    }
}