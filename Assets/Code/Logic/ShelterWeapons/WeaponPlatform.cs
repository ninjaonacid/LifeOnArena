using Code.Enemy;
using Code.Hero;
using Code.Services;
using Code.Services.Input;
using Code.StaticData;
using Code.UI.WeaponPlatform;
using UnityEngine;

namespace Code.Logic.ShelterWeapons
{
    public class WeaponPlatform : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponId weaponId;
        [SerializeField] private WeaponPlatformAbilityUi abilityView;

        public WeaponData WeaponData;
        public Transform WeaponContainer;
        public EquipWeaponButton EquipWeaponButton;
        public TriggerObserver TriggerObserver;


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
           heroWeapon.EquipWeapon(WeaponData);
        }
    }
}
