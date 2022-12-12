using UnityEngine;

namespace Code.StaticData.Ability
{
    [CreateAssetMenu(menuName = "StaticData/HeroAttack/BaseAttack", fileName = "Attack")]
    public class AttackDefinition : ScriptableObject
    {
        public float Damage;
        public float Speed;
        public float AttackRadius;
        public float AttackSpeed;
    }
}
