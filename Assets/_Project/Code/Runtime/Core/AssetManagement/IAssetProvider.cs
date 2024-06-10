using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.Core.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject InstantiateSync(string path);
        GameObject InstantiateSync(string path, Vector3 point);
        GameObject InstantiateSync(string path, Transform parent);
        void Initialize();

        UniTask<T> Load<T>(AssetReferenceGameObject assetReference, CancellationToken cancellationToken = default) where T : class;

        UniTask<T> Load<T>(AssetReferenceSprite spriteReference, CancellationToken cancellationToken = default) where T: class;
        UniTask<T> Load<T>(AssetReference assetReference, CancellationToken cancellationToken = default) where T: class;

        UniTask<T> Load<T>(string assetAddress, CancellationToken cancellationToken = default) where T : class;

        void UnloadAll();
    }
}