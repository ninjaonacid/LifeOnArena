using Code.StaticData;
using UnityEngine;

namespace Code.Hero
{
    public class HeroWeapon : MonoBehaviour
    {
        public Transform WeaponSlot;
        public HeroSkills HeroSkills;

        private GameObject _currentWeapon;


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

            HeroSkills.ChangeWeaponSkill(weaponData.abilityData);

        }
    }
}
