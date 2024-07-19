using System.Collections.Generic;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyWeapon : EntityWeapon
    {
        [SerializeField] private List<WeaponData> _possibleWeapons;
        
        public void Construct(List<WeaponData> possibleWeapons)
        {
            _possibleWeapons = possibleWeapons;
            
            if (_possibleWeapons.Count > 1)
            {
                var randomWeapon = _possibleWeapons.GetRandomElement();
                EquipWeapon(randomWeapon);
            }
            else if(_possibleWeapons.Count == 1)
            {
                EquipWeapon(_possibleWeapons[0]);
            } 
            
           
        }
    }
}
