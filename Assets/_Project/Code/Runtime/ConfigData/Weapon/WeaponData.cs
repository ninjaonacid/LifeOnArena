using System.Collections.Generic;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.StateMachine;
using Code.Runtime.Logic.Damage;
using Code.Runtime.Logic.Weapon;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.ConfigData.Weapon
{
    [CreateAssetMenu(menuName = "Configs/Equipment/Weapon", fileName = "NewWeapon")]
    public class WeaponData : ScriptableObject, IDamageSource
    {
        [SerializeField] private WeaponView _weaponPrefab;
        [SerializeField] private WeaponId _weaponId;
        [SerializeField] private VisualEffectData _hitVfx;
        [SerializeField] private SlashVisualEffectData _slashVfx;
        [SerializeField] private int _price;
        [SerializeField] private float _damage;
        [SerializeField] private WeaponFsmConfig _weaponFsmConfig;
        [SerializeField] private AnimatorOverrideController _overrideController;
        [SerializeField] private Vector3 _localRotation;
        [SerializeField] private SoundAudioFile _weaponSound;
        [SerializeField] private List<DamageType> _damageType;
        public IReadOnlyList<DamageType> DamageTypes => _damageType;
        public WeaponView WeaponView => _weaponPrefab;
        public WeaponId WeaponId => _weaponId;
        public VisualEffectData HitVisualEffect => _hitVfx;
        public SlashVisualEffectData SlashVisualEffect => _slashVfx;
        public int Price => _price;
        public float Damage => _damage;
        public WeaponFsmConfig WeaponFsmConfig => _weaponFsmConfig;
        public AnimatorOverrideController OverrideController => _overrideController;
        public Vector3 LocalRotation => _localRotation;
        public SoundAudioFile WeaponSound => _weaponSound;
    }
}