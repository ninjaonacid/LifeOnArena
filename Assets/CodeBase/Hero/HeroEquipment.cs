using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Hero
{
    public class HeroEquipment : MonoBehaviour, ISavedProgress
    {
        private GameObject currentWeapon;

        private WeaponData equippedWeapon;
        [FormerlySerializedAs("HeroStats")] public CharacterStats heroCharacterStats;
        public GameObject WeaponSlot;

        public void LoadProgress(PlayerProgress progress)
        {
            heroCharacterStats = progress.CharacterStats;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        public void EquipWeapon(WeaponData weapon)
        {
            equippedWeapon = weapon;
            if (currentWeapon != null)
                Destroy(currentWeapon);

            heroCharacterStats.BaseAttackSpeed = equippedWeapon.AttackSpeed;
            heroCharacterStats.BaseAttackRadius = equippedWeapon.AttackRadius;
            heroCharacterStats.BaseDamage = equippedWeapon.Damage;

            currentWeapon = Instantiate(equippedWeapon.WeaponPrefab, WeaponSlot.transform, 
                                        true);

            currentWeapon.transform.localPosition = Vector3.zero;

            currentWeapon.transform.localRotation = Quaternion.Euler(160, 0, 0);
        }

        public void UnEquip()
        {
        }
    }
}