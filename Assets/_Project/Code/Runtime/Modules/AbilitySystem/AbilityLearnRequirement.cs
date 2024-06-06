using System;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Modules.Requirements;

namespace Code.Runtime.Modules.AbilitySystem
{
    [Serializable]
    public class AbilityLearnRequirement : IRequirement<AbilityIdentifier>
    {
        public bool CheckRequirement(AbilityIdentifier value)
        {
            return false;
        }
    }
}