using System;
using Code.Runtime.ConfigData.Identifiers;

namespace Code.Runtime.ConfigData.StateMachine
{
    [Serializable]
    public class AbilityFsmConfig
    {
        public AbilityIdentifier AbilityIdentifier;
        public float StateDuration;
    }
}
