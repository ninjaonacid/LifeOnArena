using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.AbilityMenu
{
    public class AbilityTreeCell : MonoBehaviour
    {
        [SerializeField] private Image _abilitySlotFrame;
        [SerializeField] private Image _selectionFrame;
        
        
        public void Select()
        {
            _selectionFrame.enabled = true;
        }

        public void Deselect()
        {
            _selectionFrame.enabled = false;
        }
        
    }
}
