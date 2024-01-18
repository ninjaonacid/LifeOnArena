using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachineConfig", menuName = "Config/StateMachineConfig/EnemyStateMachineConfig")]
    public class EnemyStateMachineConfig : ScriptableObject
    {
        public float AttackDuration;
        public float HitStaggerDuration;

    }
}
