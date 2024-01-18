using System;
using Code.ConfigData.Identifiers;

namespace Code.ConfigData.StateMachine
{
    [Serializable]
    public class AbilityFsmConfig
    {
        public AbilityIdentifier AbilityIdentifier;
        public float StateDuration;
    }
}
