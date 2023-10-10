using Code.ConfigData.Ability;
using Code.Core.AssetManagement;
using Code.Core.ObjectPool;
using Code.Services.BattleService;
using Code.Services.ConfigData;
using Code.Services.RandomService;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IConfigProvider _config;
        private readonly ViewObjectPool _viewPool;
        private readonly IBattleService _battleService;
        private readonly IRandomService _random;
        private readonly IAssetProvider _assetProvider;
        public AbilityFactory(
            IAssetProvider assetProvider,
            IConfigProvider config,
            ViewObjectPool viewPool,
            IBattleService battleService,
            IRandomService random)
        {
            _assetProvider = assetProvider;
            _config = config;
            _viewPool = viewPool;
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
            ability.InitServices(_viewPool, _battleService);
            
            if (ability.ViewObjectData)
            {
                InitAbilityAssets(ability.ViewObjectData.ViewReference);
            }
            
            return ability;
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
