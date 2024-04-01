using UnityEngine;

namespace Code.Runtime.ConfigData.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachineConfig", menuName = "Config/StateMachineConfig/EnemyStateMachineConfig")]
    public class EnemyStateMachineConfig : ScriptableObject
    {
        public float AttackDuration;
        public float HitStaggerDuration;
        public float PreAttackDuration;
    }
}
