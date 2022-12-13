using Code.Hero.Abilities;
using Code.Services;
using Code.StaticData.Ability;

namespace Code.Infrastructure.Factory
{
    public interface IAbilityFactory : IService
    {
        HeroAbilityData CreateAbility(AbilityId abilityId);
    }
}