using System;
using Code.Data;
using Code.Entity.Enemy;
using Code.Entity.Hero;
using Code.Logic.EntitiesComponents;
using Code.StaticData;
using Code.UI.WeaponPlatform;
using UnityEngine;

namespace Code.Logic.ShelterWeapons
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
               _lootData.CountChanged?.Invoke();

               OnWeaponPurchase?.Invoke();

               heroWeapon.EquipWeapon(WeaponData);
           }
        }

        public void UnlockWeapon()
        {

        }

    }
}
