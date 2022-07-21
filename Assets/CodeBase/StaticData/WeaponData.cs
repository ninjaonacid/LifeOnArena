using CodeBase.UI.Inventory;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Weapon", fileName = "NewWeapon")]
    public class WeaponData : InventoryItem, IEquipable
    {
        public float AttackRadius;
        public float AttackSpeed;
        public float Damage;
        public EquipmentSlot EquipmentSlot = EquipmentSlot.Weapon;
        public GameObject WeaponPrefab;

        public EquipmentSlot GetEquipmentSlot()
        {
            return EquipmentSlot;
        }
    }
}