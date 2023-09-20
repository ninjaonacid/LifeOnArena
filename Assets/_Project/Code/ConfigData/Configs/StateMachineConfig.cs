using UnityEngine;

namespace Code.ConfigData.Configs
{
    [CreateAssetMenu(fileName = "StateMachineConfig", menuName = "StaticData/StateMachineConfig")]
    public class StateMachineConfig : ScriptableObject
    {
        public float AttackDuration;
        public float HitStaggerDuration;

    }
}
