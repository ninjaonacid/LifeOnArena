using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData;
using UnityEngine;

namespace Code.Hero
{
    public class HeroWeapon : MonoBehaviour
    {
        public Transform WeaponSlot;
        private GameObject _currentWeapon;

        private IProgressService _progressService;
        private PlayerProgress _progress => _progressService.Progress;
        public void EquipWeapon(WeaponData weaponData)
        {
            if (_currentWeapon != null)
                Destroy(_currentWeapon.gameObject);

            _currentWeapon = Instantiate(weaponData.WeaponPrefab, WeaponSlot, false);
            _currentWeapon.transform.localPosition = Vector3.zero;

            _currentWeapon.transform.localRotation = Quaternion.Euler(
                weaponData.Rotation.x,
                weaponData.Rotation.y, 
                weaponData.Rotation.z);

            _progress.SkillHolderData.ChangeSkillUI(weaponData.Ability);

        }
    }
}
