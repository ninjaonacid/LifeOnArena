using Code.StaticData;
using UnityEngine;

namespace Code.Entity
{
    public class EntityWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform _weaponPosition;
        [SerializeField] private WeaponData _weaponData;

        protected WeaponData EquippedWeapon => _weaponData;
    }
}
