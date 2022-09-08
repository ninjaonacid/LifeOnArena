using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class SkillHolder : MonoBehaviour, IPointerDownHandler
    {
        private Image _image;
        private SkillItem _skill;
        public SkillItem Skill
        {
            get => _skill;
            set
            {
                _image.sprite = value.Image.sprite;
                _skill = value;
            }
        }

        private SkillHolderContainer _skillHolderContainer;
        
        public Image Image => _image;

     
        private void Awake()
        {
            _image = GetComponent<Image>();

        }

        public void Setup(SkillHolderContainer skillHolderContainer)
        {
            _skillHolderContainer = skillHolderContainer;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _skillHolderContainer.CurrentSelectedSlot = this;
        }
    }
}
