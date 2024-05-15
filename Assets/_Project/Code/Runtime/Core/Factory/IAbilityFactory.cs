using Code.Runtime.Modules.AbilitySystem;

namespace Code.Runtime.Core.Factory
{
    public interface IAbilityFactory
    {
        ActiveAbilityBlueprintBase CreateAbilityTemplate(int abilityId);
        ActiveAbilityBlueprintBase InitializeAbilityBlueprint(ActiveAbilityBlueprintBase activeAbilityBlueprintBase);
        ActiveAbility CreateActiveAbility(int abilityId, AbilityController owner);
    }
}