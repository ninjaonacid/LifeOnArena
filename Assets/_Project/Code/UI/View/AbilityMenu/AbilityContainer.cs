using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] private AbilityItemView _abilityItem;
        private List<AbilityItemView> _abilityViews;
        private AbilityItemView _selectedItem;
        public event Action<int> OnAbilitySelected;
        public void InitializeAbilityContainer(int abilitiesCount)
        {
            _abilityViews = new List<AbilityItemView>();

            for (int i = 0; i < abilitiesCount; i++)
            {
                var abilityView = Instantiate(_abilityItem, Vector3.zero, Quaternion.identity);
                abilityView.transform.SetParent(this.gameObject.transform);
                
                _abilityViews.Add(abilityView);
            }

            foreach (var ability in _abilityViews)
            {
                ability.OnAbilityItemClick += HandleAbilitySelection;
            }
        }

        public int GetSelectedSlotIndex()
        {
            return _abilityViews.IndexOf(_selectedItem);
        }
        
        public void UpdateData(int abilityIndex, int equippedIndex, Sprite abilityIcon)
        {
            _abilityViews[abilityIndex].SetData(abilityIcon, equippedIndex);
        }

        private void HandleAbilitySelection(AbilityItemView obj)
        {
            if (_selectedItem && _selectedItem != obj)
            {
                _selectedItem.Deselect();
            }
            
            _selectedItem = obj;
            _selectedItem.Select();
            
            OnAbilitySelected?.Invoke(_abilityViews.IndexOf(obj));
        }
        
        
    }
}
