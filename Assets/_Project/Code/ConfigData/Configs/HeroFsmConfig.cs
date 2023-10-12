using UnityEngine;

namespace Code.ConfigData.Configs
{
    [CreateAssetMenu(menuName = "Config/StateMachine", fileName = "HeroFsm")]
    public class HeroFsmConfig : ScriptableObject
    {
        public float FirstAttackStateDuration;
        public float SecondAttackStateDuration;
        public float ThirdAttackStateDuration;
    }
}
