using System.Threading;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagment
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 point);
        GameObject Instantiate(string path, Transform parent);
        void Initialize();

        UniTask<T> Load<T>(AssetReferenceGameObject assetReference) where T : class;

        UniTask<T> Load<T>(AssetReferenceSprite spriteReference) where T: class;
        UniTask<T> Load<T>(AssetReference assetReference) where T: class;

        UniTask<T> Load<T>(string assetAddress) where T : class;

        void Cleanup();
    }
}