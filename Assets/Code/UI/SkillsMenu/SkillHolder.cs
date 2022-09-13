using System;
using Code.StaticData.Ability;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class SkillHolder : MonoBehaviour, IPointerDownHandler
    {
        private Image _image;
        public Image Image => _image;

        public HeroAbility_SO HeroAbility;

        public event Action<SkillHolder> OnSlotChanged;

        private SkillHolderContainer _skillHolderContainer;

        public void SetSkill(SkillItem skillItem)
        {
            HeroAbility = skillItem.HeroAbility;
            _image.sprite = HeroAbility.SkillIcon;
            _skillHolderContainer.StopFade();
            _skillHolderContainer.ResetSelection();
            OnSlotChanged?.Invoke(this);
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
