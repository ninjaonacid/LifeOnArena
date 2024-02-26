using Code.Runtime.ConfigData.Ability;
using Code.Runtime.Services;

namespace Code.Runtime.Core.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityTemplateBase CreateAbilityTemplate(int heroAbilityId);
        AbilityTemplateBase InitializeAbilityTemplate(AbilityTemplateBase ability);
    }
}