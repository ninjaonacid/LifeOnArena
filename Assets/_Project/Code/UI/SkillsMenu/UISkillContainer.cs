using Code.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.UI.SkillsMenu
{
    public class UISkillContainer : MonoBehaviour
    {
        private UISkillSlot[] _slots;
        [Inject]
        public void Construct(IProgressService progress)
        {
            foreach (UISkillSlot slot in _slots)
            {
                
            }
        }

    }
}
