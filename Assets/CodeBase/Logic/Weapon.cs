using CodeBase.Hero;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;

        private HeroInventory _heroInventory;
        private int _amount = 1;

        private void OnTriggerEnter(Collider other)
        {
            var equipment = other.gameObject.GetComponent<HeroEquipment>();

            var hero = GameObject.FindGameObjectWithTag("Player");
            _heroInventory = hero.GetComponent<HeroInventory>();
            _heroInventory.AddItemToEmptySlot(weaponData, _amount);
            Destroy(gameObject);
        }

    }
}