using Code.StaticData.Ability;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Weapon", fileName = "NewWeapon")]
    public class WeaponData : ScriptableObject
    {
        public HeroAbility Ability;
        public GameObject WeaponPrefab;
        public WeaponId WeaponId;
        public float AttackRadius;
        public float AttackSpeed;
        public float Damage;

        public Vector3 Rotation;

    }
}