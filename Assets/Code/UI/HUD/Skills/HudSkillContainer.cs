using Code.Data;
using Code.Services;
using UnityEngine;

namespace Code.UI.HUD.Skills
{
    public class HudSkillContainer : MonoBehaviour
    {
        private HudSkillButton[] _skillButtons;

        public void Construct(SkillHudData skillHudData,
            IStaticDataService staticData)
        {

            _skillButtons = GetComponentsInChildren<HudSkillButton>();

            for (int i = 0; i < _skillButtons.Length; i++)
            {
                _skillButtons[i].Construct(staticData, skillHudData);
            }
        }
    }
}
