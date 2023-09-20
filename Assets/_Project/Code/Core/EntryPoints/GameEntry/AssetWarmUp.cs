using Code.Core.AssetManagement;
using Code.Services;
using Code.Services.ConfigData;

namespace Code.Core.EntryPoints.GameEntry
{
    public class AssetWarmUp
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IConfigProvider _configProvider;

        public AssetWarmUp(IAssetProvider assetProvider, IConfigProvider configProvider)
        {
            _assetProvider = assetProvider;
            _configProvider = configProvider;
        }
    }
}
