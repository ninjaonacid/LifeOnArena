using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] private List<AbilityTreeCell> _abilityTreeCells;

        private AbilityTreeCell _selectedCell;

        private Subject<int> _abilitySelected;

        public void Initialize()
        {
            foreach (var cell in _abilityTreeCells)
            {
                cell.OnClickAsObservable().Subscribe(HandleAbilitySelection);
            }
        }

        public int GetSelectedSlotIndex()
        {
            return _abilityTreeCells.IndexOf(_selectedCell);
        }
        
        public void UpdateData(int abilityIndex, int equippedIndex, Sprite abilityIcon, bool isUnlocked)
        {
            _abilityTreeCells[abilityIndex].UpdateData(abilityIcon, isUnlocked, equippedIndex);
        }

        public IObservable<int> OnAbilitySelectedAsObservable()
        {
            return _abilitySelected ??= (_abilitySelected = new Subject<int>());
        }

        private void HandleAbilitySelection(AbilityTreeCell obj)
        {
            int selectedItemIndex = _abilityTreeCells.IndexOf(obj);
            
            var selectedAbilityCell = _abilityTreeCells[selectedItemIndex];
            
            
            if (_selectedCell is not null && _selectedCell != obj)
            {
                var previousSelectedCell = _abilityTreeCells[_abilityTreeCells.IndexOf(_selectedCell)];
                previousSelectedCell.Deselect();
            }
            
            _selectedCell = obj;
            
            selectedAbilityCell.Select();

            _abilitySelected?.OnNext(_abilityTreeCells.IndexOf(obj));
        }
        
        
    }
}
