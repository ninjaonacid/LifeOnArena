using Code.Infrastructure.AssetManagement;
using Code.Services;

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
