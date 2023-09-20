using Code.Core.Factory;
using VContainer.Unity;

namespace Code.Core.EntryPoints.GameEntry
{
    public class AssetWarmUp : IInitializable
    {
        private readonly IHeroFactory _heroFactory;

        public AssetWarmUp(IHeroFactory heroFactory)
        {
            _heroFactory = heroFactory;
        }


        public void Initialize()
        {
            _heroFactory.InitAssets();
        }
    }
}
