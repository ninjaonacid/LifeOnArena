using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] private AbilityItemView _abilityItem;
        
        private List<AbilityItemView> _abilityViews;
        private AbilityItemView _selectedItem;

        private Subject<int> _abilitySelected;
        
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
                ability.OnAbilityItemClickAsObservable().Subscribe(HandleAbilitySelection).AddTo(gameObject);
            }
        }

        public int GetSelectedSlotIndex()
        {
            return _abilityViews.IndexOf(_selectedItem);
        }
        
        public void UpdateData(int abilityIndex, int equippedIndex, Sprite abilityIcon, bool isUnlocked)
        {
            _abilityViews[abilityIndex].SetData(abilityIcon, isUnlocked, equippedIndex);
        }

        public IObservable<int> OnAbilitySelectedAsObservable()
        {
            return _abilitySelected ??= (_abilitySelected = new Subject<int>());
        }

        private void HandleAbilitySelection(AbilityItemView obj)
        {
            if (_selectedItem && _selectedItem != obj)
            {
                _selectedItem.Deselect();
            }
            
            _selectedItem = obj;
            _selectedItem.Select();
            
            _abilitySelected?.OnNext(_abilityViews.IndexOf(obj));
        }
        
        
    }
}
