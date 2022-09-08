using UnityEngine;

namespace Code.UI.SkillsMenu
{
    public class SkillListContainer : MonoBehaviour
    {
        public SkillHolderContainer SkillContainer;
        private SkillItem[] _skillItems;

        private void Awake()
        {
            _skillItems = GetComponentsInChildren<SkillItem>();

            foreach (var item in _skillItems)
            {
              item.Setup(this);
            }
        }

        public void SkillChange(SkillItem skill)
        {
            if (SkillContainer.CurrentSelectedSlot != null)
            {
                SkillContainer.CurrentSelectedSlot.Skill = skill;
            }
        }
    }
}
