
using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Weapon", fileName = "NewWeapon")]
    public class WeaponData : InventoryItem
    {
        public float AttackRadius;
        public float AttackSpeed;
        public float Damage;
        public GameObject WeaponPrefab;

  
    }
}