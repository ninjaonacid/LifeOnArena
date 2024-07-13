using System;
using System.Collections.Generic;
using System.Linq;
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

        public void UpdateData(int levelId, string locationName, Sprite icon, bool isUnlocked, bool isCompleted)
        {
            foreach (var locationPoint in _locationPoints)
            {
                if (locationPoint.LevelId.Id == levelId)
                {
                    locationPoint.UpdateData(isUnlocked, isCompleted);
                }
            }
        }

        private void SelectCurrentLevel(LocationPoint location)
        {
            _selectedLevel = location;
            location.Select();
        }

        public int GetSelectedLocationId()
        {
            if (_selectedLevel is not null)
            {
                return _selectedLevel.LevelId.Id;
            }
            else return -1;
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

            _levelSelectedSubject?.OnNext(selectedLocationPoint.LevelId.Id);
        }

        public IObservable<int> OnLevelSelectedAsObservable()
        {
            return _levelSelectedSubject ??= new Subject<int>();
        }

    }
}
