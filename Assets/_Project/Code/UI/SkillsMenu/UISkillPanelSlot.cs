using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

namespace Code.UI.SkillsMenu
{
    public class UISkillPanelSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _skillIcon;
        [SerializeField] private CanvasGroup _selectionFrame;
        [SerializeField] private AbilityTemplateBase _abilitySO;

        private IProgressService _progress;
        private UISkillPanelContainer _container;
        public void Construct(IProgressService progress, UISkillPanelContainer container)
        {
            _progress = progress;
            _container = container;
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _container.SetSelectedSlot(this);
        }

        public void ShowSelectionFrame(bool value)
        {
            if (value)
            {
                _selectionFrame.alpha = 255;
            }
            else
            {
                _selectionFrame.alpha = 0;
            }
        }


    }
}
