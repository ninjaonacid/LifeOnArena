using Code.ConfigData.Ability;
using Code.Services;

namespace Code.Core.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityTemplateBase CreateAbilityTemplate(int heroAbilityId);
    }
}