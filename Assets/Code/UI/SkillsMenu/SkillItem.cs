using System;
using Code.StaticData.Ability;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class SkillItem : MonoBehaviour, IPointerDownHandler
    {
        public Image Image;
        public HeroAbility_SO HeroAbility;
        private SkillListContainer _skillListContainer;
        public void Construct(SkillListContainer skillListContainer)
        {
            _skillListContainer = skillListContainer;
        }

        private void Awake()
        {
            Image = GetComponent<Image>();
            Image.sprite = HeroAbility.SkillIcon;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _skillListContainer.SkillChange(this);
        }
    }
}
