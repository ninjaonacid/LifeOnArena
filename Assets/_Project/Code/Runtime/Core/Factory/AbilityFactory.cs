using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.BattleService;
using Code.Runtime.Services.RandomService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.Core.Factory
{
    public class AbilityFactory : IAbilityFactory
    {
        private readonly IConfigProvider _config;
        private readonly BattleService _battleService;
        private readonly IRandomService _random;
        private readonly IAssetProvider _assetProvider;
        private readonly VisualEffectFactory _visualEffectFactory;
        private readonly ProjectileFactory _projectileFactory;
        public AbilityFactory(
            IAssetProvider assetProvider,
            IConfigProvider config,
            VisualEffectFactory visualEffectFactory,
            ProjectileFactory projectileFactory,
            BattleService battleService,
            IRandomService random)
        {
            _assetProvider = assetProvider;
            _config = config;
            _visualEffectFactory = visualEffectFactory;
            _projectileFactory = projectileFactory;
            _battleService = battleService;
            _random = random;
        }

        public ActiveAbilityBlueprintBase CreateAbilityTemplate(int heroAbilityId)
        {
            ActiveAbilityBlueprintBase activeAbilityBlueprintBaseBluePrint = _config.Ability(heroAbilityId);

            InitializeAbilityTemplate(activeAbilityBlueprintBaseBluePrint);

            return activeAbilityBlueprintBaseBluePrint;
        }
        
        public ActiveAbilityBlueprintBase InitializeAbilityTemplate(ActiveAbilityBlueprintBase activeAbilityBlueprintBase)
        {
            activeAbilityBlueprintBase.InitServices(_visualEffectFactory, _projectileFactory, _battleService);
            
            if (activeAbilityBlueprintBase.VisualEffectData)
            {
                InitAbilityAssets(activeAbilityBlueprintBase.VisualEffectData.PrefabReference).Forget();
            }
            
            return activeAbilityBlueprintBase;
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
