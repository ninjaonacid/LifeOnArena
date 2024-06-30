using System.Collections.Generic;
using Code.Runtime.ConfigData.Attack;
using Code.Runtime.ConfigData.Audio;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Logic.Damage;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Modules.Requirements;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

namespace Code.Runtime.ConfigData.Weapon
{
    [CreateAssetMenu(menuName = "Config/Equipment/Weapon", fileName = "NewWeapon")]
    public class WeaponData : SerializedScriptableObject, IDamageSource
    {
        [SerializeField] private Sprite _weaponIcon;
        [SerializeField] private WeaponView _weaponPrefab;
        [SerializeField] private WeaponView _shieldPrefab;
        [SerializeField] private WeaponId _weaponId;
        [SerializeField] private bool _isDual;
        [SerializeField] private bool _isHeroWeapon;
        [SerializeField] private bool _isWithShield;
        [SerializeField] private VisualEffectData _hitVfx;
        [SerializeField] private AnimatorOverrideController _overrideController;
        [SerializeField] private SoundAudioFile _weaponSound;
        [SerializeField] private List<DamageType> _damageType;
        [SerializeField] private List<AttackConfig> _attacks;
        [SerializeField] private IRequirement<PlayerData> _unlockRequirement;
        [SerializeField] private int _possibleCollisions;
        [SerializeField] private LocalizedString _weaponName;
        [SerializeField] private LocalizedString _weaponDescription;
        public IReadOnlyList<DamageType> DamageTypes => _damageType;
        public Sprite WeaponIcon => _weaponIcon;
        public WeaponView WeaponView => _weaponPrefab;
        public WeaponView ShieldPrefab => _shieldPrefab;
        public WeaponId WeaponId => _weaponId;
        public bool IsDual => _isDual;
        public bool IsHeroWeapon => _isHeroWeapon;
        public bool IsWithShield => _isWithShield;
        public VisualEffectData HitVisualEffect => _hitVfx;
        public AnimatorOverrideController OverrideController => _overrideController;
        public SoundAudioFile WeaponSound => _weaponSound;
        public int PossibleCollisions => _possibleCollisions;
        public IRequirement<PlayerData> UnlockRequirement => _unlockRequirement;
        public List<AttackConfig> AttacksConfigs => _attacks;
        public LocalizedString WeaponName => _weaponName;
        public LocalizedString WeaponDescription => _weaponDescription;
    }
}