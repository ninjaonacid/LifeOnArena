
using Code.Services;
using Code.StaticData.Ability;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        AbilityBluePrintBase CreateAbility(string heroAbilityId);
        PowerUp GetUpgrade();
    }
}