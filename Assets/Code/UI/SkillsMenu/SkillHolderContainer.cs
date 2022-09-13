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
    
        private IPersistentProgressService _progressService;
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

        public void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            
            _skillHolders = GetComponentsInChildren<SkillHolder>();

          
            for (var index = 0; index < _skillHolders.Length; index++)
            {
                if (_progress.SkillsData.HeroAbility[index] != null)
                {
                    _skillHolders[index].HeroAbility = _progress.SkillsData.HeroAbility[index];
                    _skillHolders[index].Image.sprite = _progress.SkillsData.HeroAbility[index].SkillIcon;
                }

                _skillHolders[index].Construct(this);
                _skillHolders[index].OnSlotChanged += UpdateProgress;
            }
        }

        private void Awake()
        {
            _saveLoad = AllServices.Container.Single<ISaveLoadService>();
        }

        public void UpdateProgress(SkillHolder skillHolder)
        {
            for (var index = 0; index < _skillHolders.Length; index++)
            {
                _progress.SkillsData.HeroAbility[index] = _skillHolders[index].HeroAbility;
            }
        }

        private void OnDestroy()
        {
            _saveLoad.SaveProgress();
            _saveLoad.SaveProgressAtPath();
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
            //_currentSlot = null;
        }

        public void StopFade()
        {
            _fade.Rewind();
            _fade.Kill();
        }
    }
}
