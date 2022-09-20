using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.Ability;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

            for (var index = 0; index < _skillHolders.Length; index++)
            {
                _skillHolders[index].AbilityId = _progress.SkillHolderData.AbilityID[index];   
                _skillHolders[index].Construct(this, _staticData);
                _skillHolders[index].OnSlotChanged += UpdateProgress;
            }
        }

        private void Awake()
        {
            _saveLoad = AllServices.Container.Single<ISaveLoadService>();
        }

        public void UpdateProgress()
        {
            for (var index = 0; index < _skillHolders.Length; index++)
            {
                if (_skillHolders[index].HeroAbility != null)
                    _progress.SkillHolderData.AbilityID[index] = _skillHolders[index].HeroAbility.AbilityId;
            }
        }

        public bool IsSkillInHolder(SkillItem skillItem)
        {
            for (int i = 0; i < _skillHolders.Length; i++)
            {
                return skillItem.HeroAbility == _skillHolders[i].HeroAbility;
            }
            return false;
        }

        public void SwapSkill(SkillItem skillItem)
        {
            for (int i = 0; i < _skillHolders.Length; i++)
            {
                if (skillItem.HeroAbility == _skillHolders[i].HeroAbility)
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
