using System.Collections.Generic;
using Code.ConfigData.Audio;
using Code.ConfigData.Configs;
using Code.ConfigData.Identifiers;
using Code.Logic.Damage;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.ConfigData
{
    [CreateAssetMenu(menuName = "StaticData/Weapon", fileName = "NewWeapon")]
    public class WeaponData : ScriptableObject, IDamageSource
    {
        public IReadOnlyList<DamageType> DamageTypes => DamageType;

        public List<DamageType> DamageType;
        public GameObject WeaponPrefab;
        public WeaponId WeaponId;
        public int Price;
        public float AttackSpeed;
        public float Damage;
        public AnimatorOverrideController OverrideController;
        public HeroFsmConfig FsmConfig;
        public Vector3 LocalRotation;
        public Vector3 LocalPosition;
        public SoundAudioFile WeaponSound;

    }
}