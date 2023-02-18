using UnityEngine;

namespace Code.StaticData
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        [Range(0.5f, 1)] public float Cleavage;

        [Range(1f, 30f)] public float Damage;

        [Range(0.5f, 5f)] public float EffectiveDistance;

        [Range(1, 100)] public int Hp;

        public float AttackDuration;

        public float HitStaggerDuration;

        public int MaxLoot;
        public int MinLoot;

        public MonsterTypeId MonsterTypeId;

        public float MoveSpeed;

        public GameObject Prefab;
    }
}