using Code.Data;
using Code.Services.PersistentProgress;
using DG.Tweening;
using UnityEngine;

namespace Code.UI.SkillsMenu
{
    public class SkillHolderContainer : MonoBehaviour, ISavedProgress
    {
        private SkillHolder[] _skillHolders;
        private SkillHolder _currentSlot;
        private Tweener _fade;
        public SkillHolder CurrentSelectedSlot
        {
            get => _currentSlot;
            set
            {
                if (value != _currentSlot)
                {
                    _currentSlot = value;
                    Debug.Log("SlotChanged");
                }
            }
        }

        private void Awake()
        {
           _skillHolders = GetComponentsInChildren<SkillHolder>();

           foreach (var skillHolder in _skillHolders)
           {
               skillHolder.Setup(this);
           }
        }

        public void ChangeSelectedSlot(SkillHolder skillHolder)
        {
            CurrentSelectedSlot = skillHolder;

            _fade = CurrentSelectedSlot.Image
                .DOFade(0, 3f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(ResetSelection);
        }

        public void ResetSelection()
        {

            CurrentSelectedSlot = null;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            for (var index = 0; index < _skillHolders.Length; index++)
            {
                var skillHolder = _skillHolders[index];
                skillHolder = progress.SkillsData.skillHolders[index];
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            for (var index = 0; index < _skillHolders.Length; index++)
            {
                var skillHolder = _skillHolders[index];
                progress.SkillsData.skillHolders[index] = skillHolder;
            }
        }
    }
}
