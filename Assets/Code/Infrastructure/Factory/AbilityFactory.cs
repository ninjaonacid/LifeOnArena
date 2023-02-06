using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.StaticData.Ability;

namespace Code.Infrastructure.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _random;

        public AbilityFactory(
            IStaticDataService staticData, 
            IProgressService progress, 
            IRandomService random)
        {
            _staticData = staticData;
            _random = random;

        }

        public AbilityBluePrintBase CreateAbility(string heroAbilityId) =>
            _staticData.ForAbility(heroAbilityId);


        //public IAbility CreateAbilitys(string heroAbilityId)
        //{
        //    var ability = _staticData.ForAbility(heroAbilityId).GetAbility();
        //}
    
    }
}
