using System.Collections.Generic;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.RandomService;
using Code.StaticData.Ability;
using Code.StaticData.Ability.PassiveAbilities;

namespace Code.Infrastructure.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _random;

        private List<PassiveAbilityTemplateBase> _passiveAbilities;
        private List<PassiveAbilityTemplateBase> _availablePassives;
        public AbilityFactory(
            IStaticDataService staticData, 
            IProgressService progress, 
            IRandomService random)
        {
            _staticData = staticData;
            _random = random;

            _passiveAbilities = new List<PassiveAbilityTemplateBase>();
            _availablePassives = new List<PassiveAbilityTemplateBase>();

            _passiveAbilities = _staticData.GetPassives();
            
        }

        public void InitFactory()
        {
        }

        public AbilityTemplateBase CreateAbility(string heroAbilityId) =>
            _staticData.ForAbility(heroAbilityId);


        public PassiveAbilityTemplateBase GetRandomPassiveAbility()
        {
            int randomIndex = _random.GetRandomNumber(_passiveAbilities.Count);

            var result = _passiveAbilities[randomIndex];
            _passiveAbilities.RemoveAt(randomIndex);
            return result;
        }
    
    }
}
