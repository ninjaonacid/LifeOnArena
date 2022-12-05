using Code.Enemy;
using Code.Hero;
using Code.Services;
using Code.Services.Input;
using Code.StaticData;
using UnityEngine;

namespace Code.Logic.ShelterWeapons
{
    public class WeaponPlatform : MonoBehaviour
    {
        [SerializeField] private WeaponId weaponId;

        public WeaponData weaponData;
        public Transform WeaponContainer;
        public EquipWeaponButton EquipWeaponButton;
        private bool _isPlayerNear;
        public TriggerObserver TriggerObserver;
        private IInputService _input;
        private HeroWeapon _heroWeapon;

        public void Construct(StaticDataService staticData)
        {
        }
        private void Update()
        {
            if (_isPlayerNear && _input.isInteractButtonUp())
            {
               _heroWeapon.EquipWeapon(weaponData);
            }
        }

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider obj)
        {
            _heroWeapon = obj.GetComponent<HeroWeapon>();
            _isPlayerNear = true;
            EquipWeaponButton.gameObject.SetActive(true);
        }

        private void TriggerExit(Collider obj)
        {
            _isPlayerNear = false;
            EquipWeaponButton.gameObject.SetActive(false);
        }
    }
}
