using System;
using Code.Runtime.ConfigData;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.Hero;
using Code.Runtime.UI.WeaponPlatform;
using UnityEngine;

namespace Code.Runtime.Logic.ShelterWeapons
{
    public class WeaponPlatform : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponPlatformAbilityUi abilityView;

        public event Action OnWeaponPurchase;
        public WeaponData WeaponData;
        public Transform WeaponContainer;
        public EquipWeaponButton EquipWeaponButton;
        public TriggerObserver TriggerObserver;
        private LootData _lootData;

        public void Construct(LootData lootData)
        {
            _lootData = lootData;
        }
        private void Awake()
        {
            abilityView.Construct(WeaponData);

            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            EquipWeaponButton.gameObject.SetActive(true);
        }

        private void TriggerExit(Collider obj)
        {
            EquipWeaponButton.gameObject.SetActive(false);
        }

        public void Interact(HeroInteraction interactor)
        {
           var heroWeapon = interactor.GetComponent<HeroWeapon>();

           if (_lootData.Collected >= WeaponData.Price)
           {
               _lootData.Collected -= WeaponData.Price;
               //_lootData.CountChanged?.Invoke();

               OnWeaponPurchase?.Invoke();

               heroWeapon.EquipWeapon(WeaponData);
           }
        }

        public void UnlockWeapon()
        {

        }

    }
}
