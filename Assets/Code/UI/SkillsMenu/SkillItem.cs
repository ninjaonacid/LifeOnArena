using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class SkillItem : MonoBehaviour, IPointerDownHandler
    {
        public Image Image;
        private SkillListContainer _skillListContainer;

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void Setup(SkillListContainer skillListContainer)
        {
            _skillListContainer = skillListContainer;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _skillListContainer.SkillChange(this);
        }
    }
}
