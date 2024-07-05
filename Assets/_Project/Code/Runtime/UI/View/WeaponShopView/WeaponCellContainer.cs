using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Code.Runtime.UI.View.WeaponShopView
{
    public class WeaponCellContainer : MonoBehaviour
    {
        [SerializeField] private List<WeaponCell> _weaponCells;
        private WeaponCell _selectedCell;
        private Subject<int> _weaponCellSelected;
        public void Initialize()
        {
            foreach (var weaponCell in _weaponCells)
            {
                weaponCell.OnClickAsObservable().Subscribe(HandleWeaponSelection);
            }
        }
        
        public void UpdateView(int elementID, string weaponName, string weaponDescription, Sprite weaponIcon, bool isUnlocked)
        {
            foreach (var weaponCell in _weaponCells)
            {
                if (elementID == weaponCell.WeaponId.Id)
                {
                    weaponCell.UpdateView(weaponIcon,weaponName, weaponDescription, isUnlocked);
                }
            }
        }
        
        public bool TryGetSelectedWeaponId(out int id)
        {
            if (_selectedCell != null)
            {
                id = _selectedCell.WeaponId.Id;
                return true;
            }

            id = -1;
            return false;
        }
        public IObservable<int> OnWeaponCellSelectedAsObservable()
        {
            return _weaponCellSelected ??= new Subject<int>();
        }
        private void HandleWeaponSelection(WeaponCell obj)
        {
            int selectedItemIndex = _weaponCells.IndexOf(obj);
            
            var selectedWeaponCell = _weaponCells[selectedItemIndex];
            
            
            if (_selectedCell is not null && _selectedCell != obj)
            {
                var previousSelectedCell = _weaponCells[_weaponCells.IndexOf(_selectedCell)];
                previousSelectedCell.Deselect();
            }
            
            _selectedCell = obj;
            
            selectedWeaponCell.Select();

            _weaponCellSelected?.OnNext(_weaponCells.IndexOf(obj));
        }

    }
}
