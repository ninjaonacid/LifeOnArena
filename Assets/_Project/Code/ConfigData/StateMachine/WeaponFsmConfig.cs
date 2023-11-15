using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine", fileName = "WeaponFsmConfig")]
    public class WeaponFsmConfig : ScriptableObject
    {
        public float FirstAttackStateDuration;
        public float SecondAttackStateDuration;
        public float ThirdAttackStateDuration;
    }
}
