using Code.Runtime.Core.AssetManagement;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using UnityEngine;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints
{
    public class CoreLoader : IInitializable
    {
        private readonly ConfigProvider _configProvider;
        private readonly IAssetProvider _assetProvider;

        public CoreLoader(ConfigProvider configProvider, IAssetProvider assetProvider)
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
