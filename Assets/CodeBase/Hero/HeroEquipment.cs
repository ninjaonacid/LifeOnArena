using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroEquipment : MonoBehaviour, ISavedProgress
    {
        private GameObject currentWeapon;

        private WeaponData equippedWeapon;
        public Stats HeroStats;
        public GameObject WeaponSlot;

        public void LoadProgress(PlayerProgress progress)
        {
            HeroStats = progress.HeroStats;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
        }

        public void EquipItem(WeaponData weapon)
        {
            equippedWeapon = weapon;
            if (currentWeapon != null)
                Destroy(currentWeapon);

            HeroStats.AttackSpeed = equippedWeapon.AttackSpeed;
            HeroStats.DamageRadius = equippedWeapon.AttackRadius;
            HeroStats.Damage = equippedWeapon.Damage;

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