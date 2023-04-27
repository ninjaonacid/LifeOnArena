using Code.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.UI.SkillsMenu
{
    public class UISkillPanelContainer : MonoBehaviour
    {
        [SerializeField] private UISkillPanelSlot[] _slots;

        private UISkillPanelSlot _selectedSlot;

        [Inject]
        public void Construct(IProgressService progress)
        {
            foreach (UISkillPanelSlot slot in _slots)
            {
                slot.Construct(progress, this);
            }
        }

        public void SetSelectedSlot(UISkillPanelSlot slot)
        {
            if (_selectedSlot != null && _selectedSlot != slot)
            {
                _selectedSlot.ShowSelectionFrame(false);
                _selectedSlot = slot;
                _selectedSlot.ShowSelectionFrame(true);
                
            }
            else
            {
                _selectedSlot = slot;
                _selectedSlot.ShowSelectionFrame(true);
            }
            
        }
    }
}
