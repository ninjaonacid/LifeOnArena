using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Modules.Requirements;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityLearnRequirement : Requirement<AbilityIdentifier>
    {
        protected override bool CheckRequirement(AbilityIdentifier value)
        {
           return value = _requiredValue;
        }
    }
}