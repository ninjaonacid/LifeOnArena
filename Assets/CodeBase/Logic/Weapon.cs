using System;
using CodeBase.Hero;
using CodeBase.Infrastructure;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;

        private HeroInventory _heroInventory;
        private int _amount = 1;

        private void Start()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            var equipment = other.gameObject.GetComponent<HeroEquipment>();
            equipment.EquipWeapon(weaponData);
            var hero = GameObject.FindGameObjectWithTag("Player");
            _heroInventory = hero.GetComponent<HeroInventory>();
            _heroInventory.AddItemToEmptySlot(weaponData, _amount);
            Destroy(gameObject);
        }
    }
}