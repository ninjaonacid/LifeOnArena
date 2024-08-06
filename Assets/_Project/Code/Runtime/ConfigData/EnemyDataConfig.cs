using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.Weapon;
using Code.Runtime.Modules.StatSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Config/Mob")]
    public class EnemyDataConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private LocalizedString _localizedName;
        [Range(0, 1000)]
        [SerializeField] private int _maxSouls;
        [Range(0, 1000)]
        [SerializeField] private int _minSouls;
        [Range(0, 1000)]
        [SerializeField] private int _maxExp;
        [Range(0, 1000)]
        [SerializeField] private int _minExp;
        [SerializeField] private List<StatDatabase> _stats;
        [SerializeField] private List<WeaponData> _weapons;
        [SerializeField] private MobIdentifier _mobId;
        [Range(5, 20)]
        [SerializeField] private float _moveSpeed;
        
        public string Name => _name;
        public LocalizedString LocalizedName => _localizedName;
        public int MaxSouls => _maxSouls;
        public int MinSouls => _minSouls;

        public int MaxExp => _maxExp;
        public int MinExp => _minExp;
        public MobIdentifier MobId => _mobId;
        public List<StatDatabase> PossibleStats => _stats;
        public List<WeaponData> PossibleWeapons => _weapons;

        public float MoveSpeed => _moveSpeed;

        public AssetReferenceGameObject PrefabReference;
        
    }
}