using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "Equipment/Weapon", fileName = "Weapon")]
    public class WeaponData : ScriptableObject
    {
        public float AttackRadius;
        public float AttackSpeed;
        public float Damage;

        public GameObject WeaponPrefab;
    }
}