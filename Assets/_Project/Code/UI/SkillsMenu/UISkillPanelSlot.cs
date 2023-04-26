using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class UISkillPanelSlot : MonoBehaviour, IPointerClickHandler
    {
        private IProgressService _progress;
        public void Construct(IProgressService progress)
        {
            _progress = progress;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public void SelectSlot()
        {

        }

        public void DeselectSlot()
        {

        }
    }
}
