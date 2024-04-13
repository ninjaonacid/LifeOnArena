using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.ConfigData.StateMachine;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Config/Mob")]
    public class EnemyDataConfig : ScriptableObject
    {
        public int MaxLoot;
        public int MinLoot;

        public int MinExp;
        public int MaxExp;

        public MobIdentifier MobId;

        public float MoveSpeed;

        public AssetReferenceGameObject PrefabReference;
        
    }
}