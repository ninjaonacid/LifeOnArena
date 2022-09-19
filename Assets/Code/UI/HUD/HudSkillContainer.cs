using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.UI.HUD
{
    public class HudSkillContainer : MonoBehaviour
    {
        private HudSkillButton[] _skillButtons;

        private IProgressService _progressService;
        private PlayerProgress _progress => _progressService.Progress;

        public void Construct(IProgressService progressService,
            IStaticDataService staticData)
        {
            _progressService = progressService;

            _skillButtons = GetComponentsInChildren<HudSkillButton>();

            for (int i = 0; i < _skillButtons.Length; i++)
            {
                _skillButtons[i].Construct(staticData.ForAbility(_progress.SkillHolderData.AbilityID[i]));
            }
        }
     
    }
}
