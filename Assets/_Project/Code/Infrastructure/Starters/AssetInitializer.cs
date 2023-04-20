using Code.Infrastructure.AssetManagment;
using Code.Infrastructure.Factory;
using VContainer.Unity;

namespace Code.Infrastructure.Starters
{
    public class AssetInitializer : IInitializable
    {
        private readonly IAssetsProvider _assetProvider;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IHeroFactory _heroFactory;

        public AssetInitializer(IEnemyFactory enemyFactory, IHeroFactory heroFactory)
        {
            _enemyFactory = enemyFactory;
            _heroFactory = heroFactory;
        }

        public void Initialize()
        {
            
            _enemyFactory.InitAssets();
            _heroFactory.InitAssets();
        }
    }
}
