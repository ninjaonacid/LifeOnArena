using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class DotAoeAbilityBlueprint : AoeAbilityBlueprint
    {
        public override ActiveAbility GetAbility()
        {
            return new DotAoeAbility(this, _castDistance, _duration, _aoeRadius);
        }
    }
}