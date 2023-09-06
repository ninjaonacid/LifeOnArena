using Code.StaticData.Configs;
using Code.StaticData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Monster")]
    public class EnemyDataConfig : ScriptableObject
    {
        public StateMachineConfig EnemyStateMachineConfig;
        
        public int MaxLoot;
        public int MinLoot;

        public MonsterTypeId MonsterTypeId;

        public float MoveSpeed;

        public AssetReferenceGameObject PrefabReference;
        
    }
}