using Code.ConfigData.Identifiers;
using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [CreateAssetMenu(menuName = "Config/StateMachine", fileName = "WeaponFsmConfig")]
    public class WeaponFsmConfig : ScriptableObject
    {
        public WeaponId WeaponId;
        public float FirstAttackStateDuration;
        public float SecondAttackStateDuration;
        public float ThirdAttackStateDuration;
    }
}
