using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.ObjectPool;
using Code.Services;
using Code.Services.BattleService;
using Code.Services.RandomService;
using Code.StaticData.Ability;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IConfigDataProvider _configData;
        private readonly IParticleObjectPool _particlePool;
        private readonly IBattleService _battleService;
        private readonly IRandomService _random;
        private readonly IAssetProvider _assetProvider;
        public AbilityFactory(
            IAssetProvider assetProvider,
            IConfigDataProvider configData,
            IParticleObjectPool particlePool,
            IBattleService battleService,
            IRandomService random)
        {
            _assetProvider = assetProvider;
            _configData = configData;
            _particlePool = particlePool;
            _battleService = battleService;
            _random = random;
        }

        public AbilityTemplateBase CreateAbilityTemplate(int heroAbilityId)
        {
            AbilityTemplateBase abilityTemplate = _configData.ForAbility(heroAbilityId);
            
            abilityTemplate.InitServices(_particlePool, _battleService);
            
            InitAbilityAssets(abilityTemplate.PrefabReference);
      
            return abilityTemplate;
        }

        private void InitAbilityAssets(AssetReference prefabReference)
        {
            if(prefabReference.RuntimeKeyIsValid()) 
                _assetProvider.Load<GameObject>(prefabReference);
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
