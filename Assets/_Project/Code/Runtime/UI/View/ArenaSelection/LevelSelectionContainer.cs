using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.ArenaSelection
{
    public class LevelSelectionContainer : MonoBehaviour
    {
        [SerializeField] private List<LevelSelectionUI> _levelSelectionItems;
        [SerializeField] private List<LocationPoint> _locationPoints;
        
        private LocationPoint _selectedLevel;
        private Subject<int> _levelSelectedSubject;

        public void Initialize()
        {
            foreach (var level in _locationPoints)
            {
                level.OnClickAsObservable().Subscribe(HandleLevelSelection);
            }
        }

        public void UpdateData(int itemIndex, string locationName, Sprite icon, bool isUnlocked)
        {
            _locationPoints[itemIndex].UpdateData(isUnlocked);
        }

        public int GetSelectedLocationId()
        {
            return _selectedLevel.LevelId.Id;
        }
        
        private void HandleLevelSelection(LocationPoint obj)
        {
            int selectedItemIndex = _locationPoints.IndexOf(obj);
            
            var selectedLocationPoint = _locationPoints[selectedItemIndex];
            
            if (_selectedLevel is not null && _selectedLevel != obj)
            {
                var previousSelectedCell = _locationPoints[_locationPoints.IndexOf(_selectedLevel)];
                previousSelectedCell.Deselect();
            }
            
            _selectedLevel = obj;
            
            selectedLocationPoint.Select();

            _levelSelectedSubject?.OnNext(_locationPoints.IndexOf(obj));
        }

        public IObservable<int> OnLevelSelectedAsObservable()
        {
            return _levelSelectedSubject ??= new Subject<int>();
        }

    }
}
