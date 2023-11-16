using System;
using Code.ConfigData.Identifiers;

namespace Code.ConfigData.StateMachine
{
    [Serializable]
    public class WeaponFsmConfig
    {
        public float FirstAttackStateDuration;
        public float SecondAttackStateDuration;
        public float ThirdAttackStateDuration;
    }
}
