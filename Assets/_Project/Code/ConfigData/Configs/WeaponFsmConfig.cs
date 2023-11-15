using UnityEngine;

namespace Code.ConfigData.Configs
{
    [CreateAssetMenu(menuName = "Config/StateMachine", fileName = "HeroFsm")]
    public class WeaponFsmConfig : ScriptableObject
    {
        public float FirstAttackStateDuration;
        public float SecondAttackStateDuration;
        public float ThirdAttackStateDuration;
    }
}
