using Code.Data;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.UI.HUD
{
    public class HudSkillContainer : MonoBehaviour
    {
        private HudSkillButton[] _skillButtons;

        private IPersistentProgressService _progressService;
        private PlayerProgress _progress => _progressService.Progress;

        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;

            _skillButtons = GetComponentsInChildren<HudSkillButton>();

            for (int i = 0; i < _skillButtons.Length; i++)
            {
                _skillButtons[i].Construct(_progress.SkillsData.HeroAbility[i]);
            }
        }
     
    }
}
