
using Code.Services;
using Code.StaticData.Ability;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        HeroAbilityData CreateAbility(HeroAbilityId heroAbilityId);
        PowerUp GetUpgrade();
    }
}