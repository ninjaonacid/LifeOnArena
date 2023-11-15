using System;
using Code.ConfigData.Identifiers;
using UnityEngine;

namespace Code.ConfigData.StateMachine
{
    [Serializable]
    public class AbilityFsmConfig
    {
        public AbilityIdentifier AbilityIdentifier;
        public float StateDuration;
    }
}
