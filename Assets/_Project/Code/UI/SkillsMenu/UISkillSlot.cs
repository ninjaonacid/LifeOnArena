using Code.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI.SkillsMenu
{
    public class UISkillSlot : MonoBehaviour, IPointerClickHandler
    {
        private IProgressService _progress;
        public void Construct(IProgressService progress)
        {
            _progress = progress;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
}
