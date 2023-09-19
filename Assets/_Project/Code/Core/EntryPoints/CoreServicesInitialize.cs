using Code.Infrastructure.AssetManagement;
using Code.Services;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class CoreServicesInitialize : IInitializable
    {
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;

        public CoreServicesInitialize(IConfigProvider configProvider, IAssetProvider assetProvider)
        {
            _configProvider = configProvider;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
            _configProvider.Load();
            _assetProvider.Initialize();
        }
    }
}
