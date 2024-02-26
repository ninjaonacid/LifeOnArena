using Code.Runtime.Modules.AbilitySystem;

namespace Code.Runtime.Core.Factory
{
    public interface IAbilityFactory
    {
        AbilityTemplateBase CreateAbilityTemplate(int heroAbilityId);
        AbilityTemplateBase InitializeAbilityTemplate(AbilityTemplateBase ability);
    }
}