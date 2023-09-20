using Code.ConfigData;
using UnityEngine;

namespace Code.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] private WeaponData _weaponData;

        private WeaponData EquippedWeapon => _weaponData;

        public WeaponData GetEquippedWeapon()
        {
            return EquippedWeapon;
        }
    }
}
