using System;

namespace Code.Runtime.ConfigData.StateMachine
{
    [Serializable]
    public struct WeaponFsmConfig
    {
        public float FirstAttackStateDuration;
        public float SecondAttackStateDuration;
        public float ThirdAttackStateDuration;
    }
}
