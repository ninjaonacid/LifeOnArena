using UnityEngine;

namespace Code.StaticData.Configs
{
    [CreateAssetMenu(fileName = "StateMachineConfig", menuName = "StaticData/StateMachineConfig")]
    public class StateMachineConfig : ScriptableObject
    {
        public float AttackDuration;
        public float HitStaggerDuration;

    }
}
