using Code.Data;
using Code.Enemy;
using Code.Hero;
using Code.Services.PersistentProgress;
using Code.StaticData;
using Code.UI.WeaponPlatform;
using UnityEngine;

namespace Code.Logic.ShelterWeapons
{
    public class WeaponPlatform : MonoBehaviour, IInteractable, ISave
    {
        [SerializeField] private WeaponId weaponId;
        [SerializeField] private WeaponPlatformAbilityUi abilityView;

        public WeaponData WeaponData;
        public Transform WeaponContainer;
        public EquipWeaponButton EquipWeaponButton;
        public TriggerObserver TriggerObserver;
        private LootData _lootData;
        private bool _isOwned;

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
               _isOwned = true;
               heroWeapon.EquipWeapon(WeaponData);
           }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _lootData = progress.WorldData.LootData;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}
