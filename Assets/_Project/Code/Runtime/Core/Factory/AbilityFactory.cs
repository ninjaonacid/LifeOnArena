using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.Core.Factory
{
    public class AbilityFactory
    {
        private readonly ConfigProvider _config;
        private readonly BattleService _battleService;
        private readonly IAssetProvider _assetProvider;
        private readonly VisualEffectFactory _visualEffectFactory;
        private readonly ProjectileFactory _projectileFactory;
        private readonly AudioService _audioService;
        public AbilityFactory(
            IAssetProvider assetProvider,
            ConfigProvider config,
            VisualEffectFactory visualEffectFactory,
            ProjectileFactory projectileFactory,
            BattleService battleService,
            AudioService audioService)
        {
            _assetProvider = assetProvider;
            _config = config;
            _visualEffectFactory = visualEffectFactory;
            _projectileFactory = projectileFactory;
            _battleService = battleService;
            _audioService = audioService;
        }

        public ActiveAbilityBlueprintBase CreateAbilityTemplate(int abilityId)
        {
            ActiveAbilityBlueprintBase activeAbilityBluePrint = _config.Ability(abilityId);

            InitializeAbilityBlueprint(activeAbilityBluePrint);

            return activeAbilityBluePrint;
        }
        
        public ActiveAbility CreateActiveAbility(int abilityId, AbilityController owner)
        {
            ActiveAbilityBlueprintBase activeAbilityBluePrint = _config.Ability(abilityId);

            InitializeAbilityBlueprint(activeAbilityBluePrint);

            var abilityInstance = activeAbilityBluePrint.GetAbility();
            
            abilityInstance.InjectServices(_visualEffectFactory, _projectileFactory, _battleService, _audioService);
            
            abilityInstance.Initialize(owner);
            
            return abilityInstance;
        }
        
        public ActiveAbilityBlueprintBase InitializeAbilityBlueprint(ActiveAbilityBlueprintBase activeAbilityBlueprintBase)
        {

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
    }
}
