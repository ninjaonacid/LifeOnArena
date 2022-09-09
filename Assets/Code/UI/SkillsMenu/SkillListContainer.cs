using UnityEngine;

namespace Code.UI.SkillsMenu
{
    public class SkillListContainer : MonoBehaviour
    {
        public SkillHolderContainer SkillHolderContainer;
        private SkillItem[] _skillItems;

        private void Awake()
        {
            _skillItems = GetComponentsInChildren<SkillItem>();

            Setup();
        }

        public void SkillChange(SkillItem skill)
        {
            if (SkillHolderContainer.CurrentSelectedSlot != null)
            {
                SkillHolderContainer.CurrentSelectedSlot.SetSkill(skill);
            }
        }

        private void Setup()
        {
            foreach (SkillItem skillItem in _skillItems)
            {
                skillItem.Construct(this);
            }
        }
    }
}
