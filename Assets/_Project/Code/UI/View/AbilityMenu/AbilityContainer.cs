using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.UI.View.AbilityMenu
{
    public class AbilityContainer : MonoBehaviour
    {
        [SerializeField] private AbilityItemView _abilityItem;
        private List<AbilityItemView> _abilityViews;
        private AbilityItemView _selectedItem;
        private Subject<int> _subject;
        
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
        
        public void UpdateData(int abilityIndex, int equippedIndex, Sprite abilityIcon, bool isUnlocked)
        {
            _abilityViews[abilityIndex].SetData(abilityIcon, isUnlocked, equippedIndex);
        }

        private void HandleAbilitySelection(AbilityItemView obj)
        {
            if (_selectedItem && _selectedItem != obj)
            {
                _selectedItem.Deselect();
            }
            
            _selectedItem = obj;
            _selectedItem.Select();
            
            _subject.OnNext(_abilityViews.IndexOf(obj));
        }

        public IObservable<int> OnSelectionAsObservable()
        {
            return _subject ??= (_subject = new Subject<int>());
        }


    }
}
