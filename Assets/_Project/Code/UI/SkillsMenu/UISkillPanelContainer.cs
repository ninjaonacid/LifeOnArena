using Code.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.UI.SkillsMenu
{
    public class UISkillPanelContainer : MonoBehaviour
    {
        [SerializeField] private UISkillPanelSlot[] _slots;

        [Inject]
        public void Construct(IProgressService progress)
        {
            foreach (UISkillPanelSlot slot in _slots)
            {
                
            }
        }

    }
}
