using Code.Core.AssetManagement;
using Code.Services.ConfigData;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class CoreEntryPoint : IInitializable
    {
        private readonly IConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;

        public CoreEntryPoint(IConfigProvider configProvider, IAssetProvider assetProvider)
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
