using Code.Data;
using Code.Hero.Abilities;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;

namespace Code.Infrastructure.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IStaticDataService _staticData;
        private IProgressService _progress;
       
        public AbilityFactory(IStaticDataService staticData, IProgressService progress)
        {
            _staticData = staticData;
            _progress = progress;
        }

        public void LoadAbilityData(AbilityId abilityId)
        {
            HeroAbilityData abilityData = _staticData.ForAbility(abilityId);

        }

        public HeroAbilityData CreateAbility(AbilityId abilityId) =>
            _staticData.ForAbility(abilityId);
        

        

    }
}
