using System;
using Code.Services;
using Code.StaticData.Ability;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    [RequireComponent(typeof(Image))]
    public class SkillHolder : MonoBehaviour, IPointerDownHandler
    {
        private Image _image;
        public Image Image => _image;
        public int SlotId;
        public AbilityId AbilityId = AbilityId.Empty;
        public HeroAbilityData heroAbilityData;
        private SkillHolderIcon _skillHolderIcon;
        public event Action OnSlotChanged;

        private SkillHolderContainer _skillHolderContainer;

        private void Awake()
        {
            _skillHolderIcon = GetComponentInChildren<SkillHolderIcon>();
            _image = GetComponent<Image>();
        }

        public void Construct(SkillHolderContainer skillHolderContainer, IStaticDataService staticData)
        {
            _skillHolderContainer = skillHolderContainer;
            heroAbilityData = staticData.ForAbility(AbilityId);
            if (heroAbilityData != null)
            {
                _skillHolderIcon.Image.sprite = heroAbilityData.SkillIcon;
                _skillHolderIcon.Image.enabled = true;
            }
            else
            {
                _skillHolderIcon.Image.enabled = false;
            }
        }

        public void SetSkill(SkillItem skillItem)
        {
            /*if (_skillHolderContainer.IsSkillInHolder(skillItem))
            {
                _skillHolderContainer.SwapSkill(skillItem);
                return;
            }*/
            heroAbilityData = skillItem.heroAbilityData;
            AbilityId = skillItem.heroAbilityData.AbilityId;
            _skillHolderIcon.Image.sprite = heroAbilityData.SkillIcon;
            _skillHolderIcon.Image.enabled = true;
            _skillHolderContainer.StopFade();
            _skillHolderContainer.ResetSelection();
            OnSlotChanged?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _skillHolderContainer.CurrentSelectedSlot = this;
        }
    }
}
