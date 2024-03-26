using System;
using System.Collections.Generic;
using Code.Runtime.UI.AbilityMenu;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] private AbilityItemView _abilityItem;
        [SerializeField] private List<AbilityTreeCell> _abilityTreeCells;
        
        private List<AbilityItemView> _abilityItems;
        
        private AbilityItemView _selectedItem;

        private Subject<int> _abilitySelected;
        
        public void InitializeAbilityContainer(int abilitiesCount)
        {
            _abilityItems = new List<AbilityItemView>();

            for (int i = 0; i < _abilityTreeCells.Count; i++)
            {
                var abilityView = Instantiate(_abilityItem, _abilityTreeCells[i].transform);
                abilityView.transform.SetAsFirstSibling();
                

                _abilityItems.Add(abilityView);
            }

            // for (int i = 0; i < abilitiesCount; i++)
            // {
            //     var abilityView = Instantiate(_abilityItem, gameObject.transform);
            //
            //     _abilityViews.Add(abilityView);
            // }

            foreach (var ability in _abilityItems)
            {
                ability.OnAbilityItemClickAsObservable().Subscribe(HandleAbilitySelection).AddTo(gameObject);
            }
        }

        public int GetSelectedSlotIndex()
        {
            return _abilityItems.IndexOf(_selectedItem);
        }
        
        public void UpdateData(int abilityIndex, int equippedIndex, Sprite abilityIcon, bool isUnlocked)
        {
            _abilityItems[abilityIndex].SetData(abilityIcon, isUnlocked, equippedIndex);
        }

        public IObservable<int> OnAbilitySelectedAsObservable()
        {
            return _abilitySelected ??= (_abilitySelected = new Subject<int>());
        }

        private void HandleAbilitySelection(AbilityItemView obj)
        {
            int selectedItemIndex = _abilityItems.IndexOf(obj);
            var abilityCell = _abilityTreeCells[selectedItemIndex];
            
            if (_selectedItem is not null && _selectedItem != obj)
            {
                abilityCell.Deselect();
            }
            
            _selectedItem = obj;
            
            abilityCell.Select();

            _abilitySelected?.OnNext(_abilityItems.IndexOf(obj));
        }
        
        
    }
}
