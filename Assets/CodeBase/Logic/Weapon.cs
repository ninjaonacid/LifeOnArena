using CodeBase.Hero;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;


        private void OnTriggerEnter(Collider other)
        {
            var equipment = other.gameObject.GetComponent<HeroEquipment>();
            equipment.EquipWeapon(weaponData);
            Destroy(gameObject);
        }
    }
}