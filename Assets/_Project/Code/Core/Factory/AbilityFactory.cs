using Code.ConfigData.Ability;
using Code.ConfigData.StatusEffect;
using Code.Core.AssetManagement;
using Code.Entity.StatusEffects;
using Code.Services.BattleService;
using Code.Services.ConfigData;
using Code.Services.RandomService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IConfigProvider _config;
        private readonly IBattleService _battleService;
        private readonly IRandomService _random;
        private readonly IAssetProvider _assetProvider;
        private readonly ParticleFactory _particleFactory;
        public AbilityFactory(
            IAssetProvider assetProvider,
            IConfigProvider config,
            ParticleFactory particleFactory,
            IBattleService battleService,
            IRandomService random)
        {
            _assetProvider = assetProvider;
            _config = config;
            _particleFactory = particleFactory;
            _battleService = battleService;
            _random = random;
        }

        public AbilityTemplateBase CreateAbilityTemplate(int heroAbilityId)
        {
            AbilityTemplateBase abilityTemplate = _config.Ability(heroAbilityId);

            InitializeAbilityTemplate(abilityTemplate);
      
            return abilityTemplate;
        }
        
        public AbilityTemplateBase InitializeAbilityTemplate(AbilityTemplateBase ability)
        {
            ability.InitServices(_particleFactory, _battleService);
            
            if (ability.VfxData)
            {
                InitAbilityAssets(ability.VfxData.PrefabReference).Forget();
            }
            
            return ability;
        }

        private async UniTaskVoid InitAbilityAssets(AssetReference prefabReference)
        {
            if(prefabReference.RuntimeKeyIsValid()) 
                await _assetProvider.Load<GameObject>(prefabReference);
        }
        

        //public PassiveAbilityTemplateBase GetRandomPassiveAbility()
        //{
        //    int randomIndex = _random.GetRandomNumber(_passiveAbilities.Count);

        //    var result = _passiveAbilities[randomIndex];
        //    _passiveAbilities.RemoveAt(randomIndex);
        //    return result;
        //}


    }
}
