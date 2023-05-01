using System.Collections.Generic;
using Code.Infrastructure.ObjectPool;
using Code.Services;
using Code.Services.BattleService;
using Code.Services.RandomService;
using Code.StaticData.Ability;
using Code.StaticData.Ability.PassiveAbilities;

namespace Code.Infrastructure.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IParticleObjectPool _particlePool;
        private readonly IBattleService _battleService;
        private readonly IRandomService _random;
        private List<PassiveAbilityTemplateBase> _passiveAbilities;
        private List<PassiveAbilityTemplateBase> _availablePassives;

        public AbilityFactory(
            IStaticDataService staticData,
            IParticleObjectPool particlePool,
            IBattleService battleService,
            IRandomService random)
        {
            _staticData = staticData;
            _particlePool = particlePool;
            _battleService = battleService;
            _random = random;

            _passiveAbilities = new List<PassiveAbilityTemplateBase>();
            _availablePassives = new List<PassiveAbilityTemplateBase>();
        }

        public AbilityTemplateBase CreateAbilityTemplate(string heroAbilityId)
        {
            var abilityTemplate = _staticData.ForAbility(heroAbilityId);
            abilityTemplate.InitServices(_particlePool, _battleService);
            return abilityTemplate;
        }

        public PassiveAbilityTemplateBase CreatePassive(string abilityId) =>
            _staticData.ForPassiveAbility(abilityId);

        public PassiveAbilityTemplateBase GetRandomPassiveAbility()
        {
            int randomIndex = _random.GetRandomNumber(_passiveAbilities.Count);

            var result = _passiveAbilities[randomIndex];
            _passiveAbilities.RemoveAt(randomIndex);
            return result;
        }
    
    }
}
