using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class SkillHolder : MonoBehaviour, IPointerDownHandler
    {
        private Image _image;
        public Image Image => _image;

        public SkillItem Skill;

        public event Action OnSlotChanged;

        private SkillHolderContainer _skillHolderContainer;

        public void SetSkill(SkillItem skillItem)
        {
            Skill = skillItem;
            Image.sprite = Skill.Image.sprite;
            _skillHolderContainer.StopFade();
            _skillHolderContainer.ResetSelection();
            OnSlotChanged?.Invoke();
        }
        public void Construct(SkillHolderContainer skillHolderContainer)
        {
            _skillHolderContainer = skillHolderContainer;
        }

        private void Awake()
        {
            _image = GetComponent<Image>();

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _skillHolderContainer.CurrentSelectedSlot = this;
        }
    }
}
