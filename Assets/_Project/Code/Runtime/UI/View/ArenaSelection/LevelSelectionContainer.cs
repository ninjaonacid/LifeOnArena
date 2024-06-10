using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class LevelSelectionContainer : MonoBehaviour
    {
        [SerializeField] private List<LevelSelectionUI> _levelSelectionItems;
        
        private LevelSelectionUI _selectedLevel;
        private Subject<int> _levelSelectedSubject;

        public void Initialize()
        {
            foreach (var level in _levelSelectionItems)
            {
                level.OnClickAsObservable().Subscribe(HandleLevelSelection);
            }
        }

        public void UpdateData(int itemIndex, string locationName, Sprite icon, bool isUnlocked)
        {
            _levelSelectionItems[itemIndex].UpdateData(icon, locationName, isUnlocked);
        }
        
        private void HandleLevelSelection(LevelSelectionUI obj)
        {
            
            int selectedItemIndex = _levelSelectionItems.IndexOf(obj);
            
            var selectedAbilityCell = _levelSelectionItems[selectedItemIndex];
            
            if (_selectedLevel is not null && _selectedLevel != obj)
            {
                var previousSelectedCell = _levelSelectionItems[_levelSelectionItems.IndexOf(_selectedLevel)];
                previousSelectedCell.Deselect();
            }
            
            _selectedLevel = obj;
            
            selectedAbilityCell.Select();

            _levelSelectedSubject?.OnNext(_levelSelectionItems.IndexOf(obj));
        }

        public IObservable<int> OnLevelSelectedAsObservable()
        {
            return _levelSelectedSubject ??= new Subject<int>();
        }

    }
}
