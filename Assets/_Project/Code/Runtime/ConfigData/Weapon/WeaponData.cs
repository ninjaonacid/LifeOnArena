using System.Collections.Generic;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Logic.Damage;
using Code.Runtime.Logic.Weapon;
using Sirenix.OdinInspector;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.ConfigData.Weapon
{
    [CreateAssetMenu(menuName = "Config/Equipment/Weapon", fileName = "NewWeapon")]
    public class WeaponData : ScriptableObject, IDamageSource
    {
        [SerializeField] private Sprite _weaponIcon;
        [SerializeField] private WeaponView _weaponPrefab;
        [SerializeField] private WeaponId _weaponId;
        [SerializeField] private bool _isDual;
        [SerializeField] private VisualEffectData _hitVfx;
        [SerializeField] private AnimatorOverrideController _overrideController;
        [SerializeField] private Vector3 _localScale;
        [SerializeField] private SoundAudioFile _weaponSound;
        [SerializeField] private List<DamageType> _damageType;
        [SerializeField] private List<AttackConfig> _attacks;
        public IReadOnlyList<DamageType> DamageTypes => _damageType;

        public Sprite WeaponIcon => _weaponIcon;
        public WeaponView WeaponView => _weaponPrefab;
        public WeaponId WeaponId => _weaponId;
        public bool IsDual => _isDual;
        public VisualEffectData HitVisualEffect => _hitVfx;
        public AnimatorOverrideController OverrideController => _overrideController;
        public Vector3 LocalScale => _localScale;
        public SoundAudioFile WeaponSound => _weaponSound;

        public List<AttackConfig> AttacksConfigs => _attacks;
    }
}