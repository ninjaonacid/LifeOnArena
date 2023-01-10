using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using DG.Tweening;
using UnityEngine;

namespace Code.UI.SkillsMenu
{
    public class SkillHolderContainer : MonoBehaviour
    {
        private SkillHolder[] _skillHolders;
        private SkillHolder _currentSlot;
        private Tweener _fade;
    
        private IProgressService _progressService;
        private IStaticDataService _staticData;
        private PlayerProgress _progress => _progressService.Progress;
        private ISaveLoadService _saveLoad;
        public SkillHolder CurrentSelectedSlot
        {
            get => _currentSlot;
            set
            {
                if (value != _currentSlot && value != null)
                {
                    _currentSlot = value;
                    StopFade();
                    FadeSelectedSlot();
                }
            }
        }

        public void Construct(IProgressService progressService, IStaticDataService staticData)
        {
            _progressService = progressService;
            _staticData = staticData;

            _skillHolders = GetComponentsInChildren<SkillHolder>();
        }

        private void Awake()
        {
            _saveLoad = ServiceLocator.Container.Single<ISaveLoadService>();
        }



        public bool IsSkillInHolder(SkillItem skillItem)
        {
            for (int i = 0; i < _skillHolders.Length; i++)
            {
                return skillItem.heroAbilityData == _skillHolders[i].heroAbilityData;
            }
            return false;
        }

        public void SwapSkill(SkillItem skillItem)
        {
            for (int i = 0; i < _skillHolders.Length; i++)
            {
                if (skillItem.heroAbilityData == _skillHolders[i].heroAbilityData)
                {
                    
                }
            }
        }
        public void FadeSelectedSlot()
        {
            _fade = _currentSlot.Image
                .DOFade(0, 3f)
                .SetEase(Ease.InFlash, 20, 0)
                .OnComplete(ResetSelection);
        }

        public void ResetSelection()
        {
            _currentSlot = null;
        }

        public void StopFade()
        {
            _fade.Rewind();
            _fade.Kill();
        }
    }
}
