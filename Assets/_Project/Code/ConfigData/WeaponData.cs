using System.Collections.Generic;
using Code.Logic.Damage;
using Code.StaticData.Ability;
using Code.StaticData.Identifiers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Weapon", fileName = "NewWeapon")]
    public class WeaponData : ScriptableObject, IDamageSource
    {
        public IReadOnlyList<DamageType> DamageTypes => DamageType;

        public List<DamageType> DamageType;
        public GameObject WeaponPrefab;
        public WeaponId WeaponId;
        public int Price;
        public float AttackRadius;
        public float AttackSpeed;
        public float Damage;
        public Vector3 Rotation;
    }
}