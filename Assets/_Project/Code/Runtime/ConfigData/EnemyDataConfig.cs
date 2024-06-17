using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Config/Mob")]
    public class EnemyDataConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [Range(0, 1000)]
        [SerializeField] private int _maxSouls;
        [Range(0, 1000)]
        [SerializeField] private int _minSouls;
        [Range(0, 1000)]
        [SerializeField] private int _maxExp;
        [Range(0, 1000)]
        [SerializeField] private int _minExp;
        
        [SerializeField] private MobIdentifier _mobId;
        [Range(5, 20)]
        [SerializeField] private float _moveSpeed;
        
        public string Name => _name;
        public int MaxSouls => _maxSouls;
        public int MinSouls => _minSouls;

        public int MaxExp => _maxExp;
        public int MinExp => _minExp;
        public MobIdentifier MobId => _mobId;

        public float MoveSpeed => _moveSpeed;

        public AssetReferenceGameObject PrefabReference;
        
    }
}