using Code.Data;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.SkillsMenu
{
    public class SkillHolderContainer : MonoBehaviour
    {
        private SkillHolder[] _skillHolders;
        private SkillHolder _currentSlot;
        private Tweener _fade;
    
        private IPersistentProgressService _progressService;
        private ISaveLoadService saveLoad;
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

            for (var index = 0; index < _skillHolders.Length; index++)
            {
                _skillHolders[index] = _progressService.Progress.SkillsData.skillHolders[index];
            }
            
        }

        private void Awake()
        {
            saveLoad = AllServices.Container.Single<ISaveLoadService>();
           _skillHolders = GetComponentsInChildren<SkillHolder>();
           foreach (var skillHolder in _skillHolders)
           {
               skillHolder.Construct(this);
               skillHolder.OnSlotChanged += UpdateProgress;
           }
        }

        public void UpdateProgress()
        {
            Debug.Log("EventTriggeered");
            for (var index = 0; index < _skillHolders.Length; index++)
            {
                var skillHolder = _skillHolders[index];
                _progressService.Progress.SkillsData.skillHolders[index] = _skillHolders[index];
            }
            saveLoad.SaveProgress();
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
