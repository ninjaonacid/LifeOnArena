using System.Threading.Tasks;
using Code.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagment
{
    public interface IAssetsProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 point);
        GameObject Instantiate(string path, Transform parent);
        void Initialize();

        Task<T> Load<T>(AssetReferenceGameObject assetReference) where T : class;

        Task<T> Load<T>(AssetReferenceSprite spriteReference) where T: class;

        Task<T> Load<T>(string assetAddress) where T : class;

        void Cleanup();
    }
}