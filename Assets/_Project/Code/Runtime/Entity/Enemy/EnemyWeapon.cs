using System.Collections.Generic;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyWeapon : EntityWeapon
    {
        [SerializeField] private List<WeaponData> _possibleWeapons;
        protected override void Start()
        {
            if (_possibleWeapons.Count > 0)
            {
                var randomWeaponData = _possibleWeapons.GetRandomElement();
                EquipWeapon(randomWeaponData);
            }
            else
            {
                EquipWeapon(_weaponData);
            }
        }
    }
}
