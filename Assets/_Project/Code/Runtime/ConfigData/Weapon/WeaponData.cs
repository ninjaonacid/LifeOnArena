using System.Collections.Generic;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.Logic.Damage;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.ConfigData.Weapon
{
    [CreateAssetMenu(menuName = "StaticData/Weapon", fileName = "NewWeapon")]
    public class WeaponData : ScriptableObject, IDamageSource
    {
        public IReadOnlyList<DamageType> DamageTypes => DamageType;
        public List<DamageType> DamageType;
        public GameObject WeaponPrefab;
        public WeaponId WeaponId;
        public VfxData HitVfx;
        public int Price;
        public float AttackSpeed;
        public float Damage;
        public WeaponFsmConfig WeaponFsmConfig;
        public AnimatorOverrideController OverrideController;
        public Vector3 LocalRotation;
        public Vector3 LocalPosition;
        public SoundAudioFile WeaponSound;

    }
}