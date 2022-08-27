using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.UI.Inventory;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroEquipment : MonoBehaviour, ISavedProgress
    {
        private GameObject _currentWeapon;
        private WeaponData equippedWeapon;
        private CharacterStats _heroCharacterStats;


        public GameObject WeaponSlot;
        
        private readonly Dictionary<EquipmentSlot, IEquipable> _equippedItems =
         new Dictionary<EquipmentSlot, IEquipable>();

        public event Action OnEquipmentChanged;

        public void EquipItem(EquipmentSlot equipmentSlot, IEquipable item)
        {
            if (item.GetEquipmentSlot() == equipmentSlot)
            {
                _equippedItems[equipmentSlot] = item;

                OnEquipmentChanged?.Invoke();
            }
        }

        public IEquipable GetItemInSlot(EquipmentSlot equipmentSlot)
        {
            if (!_equippedItems.ContainsKey(equipmentSlot))
            {
                return null;
            }
            return _equippedItems[equipmentSlot];
        }
        public void EquipWeapon(WeaponData weapon)
        {
            equippedWeapon = weapon;
            if (_currentWeapon != null)
                Destroy(_currentWeapon);

            _heroCharacterStats.BaseAttackSpeed = equippedWeapon.AttackSpeed;
            _heroCharacterStats.BaseAttackRadius = equippedWeapon.AttackRadius;
            _heroCharacterStats.BaseDamage = equippedWeapon.Damage;

            _currentWeapon = Instantiate(equippedWeapon.WeaponPrefab, WeaponSlot.transform, 
                                        true);

            _currentWeapon.transform.localPosition = Vector3.zero;

            _currentWeapon.transform.localRotation = Quaternion.Euler(160, 0, 0);
        }

        public void UnEquip()
        {
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroCharacterStats = progress.CharacterStats;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}