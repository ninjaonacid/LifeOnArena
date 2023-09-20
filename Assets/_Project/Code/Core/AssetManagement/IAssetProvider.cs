using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject InstantiateSync(string path);
        GameObject InstantiateSync(string path, Vector3 point);
        GameObject InstantiateSync(string path, Transform parent);
        void Initialize();

        UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class;

        UniTask<T> Load<T>(AssetReferenceSprite spriteReference) where T: class;
        UniTask<T> Load<T>(AssetReference assetReference) where T: class;

        UniTask<T> Load<T>(string assetAddress) where T : class;

        void UnloadAll();
    }
}