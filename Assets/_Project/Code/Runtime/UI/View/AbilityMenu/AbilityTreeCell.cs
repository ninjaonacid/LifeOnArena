using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityTreeCell : MonoBehaviour
    {
        [SerializeField] private Image _abilitySlotFrame;
        [SerializeField] private Image _selectionFrame;
        [SerializeField] private AbilityTreeLinePathUI _line;
        
        
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
