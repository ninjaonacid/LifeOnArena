using Code.ConfigData.Configs;
using Code.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Code.ConfigData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Monster")]
    public class EnemyDataConfig : ScriptableObject
    {
        public StateMachineConfig EnemyStateMachineConfig;
        
        public int MaxLoot;
        public int MinLoot;

        [FormerlySerializedAs("MonsterTypeId")] public MobId MobId;

        public float MoveSpeed;

        public AssetReferenceGameObject PrefabReference;
        
    }
}