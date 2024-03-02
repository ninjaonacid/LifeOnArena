using Code.Runtime.Modules.AbilitySystem;

namespace Code.Runtime.Core.Factory
{
    public interface IAbilityFactory
    {
        ActiveAbilityBlueprintBase CreateAbilityTemplate(int heroAbilityId);
        ActiveAbilityBlueprintBase InitializeAbilityTemplate(ActiveAbilityBlueprintBase activeAbilityBlueprintBase);
    }
}