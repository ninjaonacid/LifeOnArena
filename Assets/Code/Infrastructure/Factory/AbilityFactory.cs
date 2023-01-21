using System.Collections.Generic;
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

        private List<PowerUp> _heroUpgrades = new List<PowerUp>();

        public AbilityFactory(IStaticDataService staticData, IProgressService progress, IRandomService random)
        {
            _staticData = staticData;
            _random = random;
            SetUpgradeList();

        }
        public HeroAbilityData CreateAbility(HeroAbilityId heroAbilityId) =>
            _staticData.ForAbility(heroAbilityId);

        private void SetUpgradeList() =>
            _heroUpgrades = _staticData.GetUpgrades();
        
        
        public PowerUp GetUpgrade()
        {
            return _heroUpgrades[_random.GetRandomNumber(_heroUpgrades.Count)];
        }
    }
}
