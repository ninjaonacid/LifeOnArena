using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachineConfig", menuName = "StaticData/StateMachineConfig")]
    public class StateMachineConfig : ScriptableObject
    {
        public float AttackDuration;
        public float HitStaggerDuration;

    }
}
