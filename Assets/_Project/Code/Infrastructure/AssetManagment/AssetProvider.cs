using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Infrastructure.AssetManagment
{
    public class AssetProvider : IAssetsProvider
    {
        private Dictionary<string, AsyncOperationHandle> _completedCache =
            new Dictionary<string, AsyncOperationHandle>();

        private Dictionary<string, List<AsyncOperationHandle>> _handles = 
            new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

 
        public async Task<T> Load<T>(AssetReferenceGameObject assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, 
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(assetReference),
                assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(AssetReferenceSprite spriteReference) where T : class
        {
            if (_completedCache.TryGetValue(spriteReference.AssetGUID,
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(spriteReference),
                spriteReference.AssetGUID);
        }


        public async Task<T> Load<T>(string assetAddress) where T : class
        {
            if (_completedCache.TryGetValue(assetAddress,
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(assetAddress),
                assetAddress);
        }


        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 point)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, point, Quaternion.identity);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }

            _completedCache.Clear();
            _handles.Clear();
        }

        private async Task<T> LoadWithCache<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += operation => { _completedCache[cacheKey] = operation; };

            AddHandleOperation(handle, cacheKey);

            return await handle.Task;
        }

        private void AddHandleOperation<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            if (!_handles.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourcesHandles))
            {
                resourcesHandles = new List<AsyncOperationHandle>();
                _handles[cacheKey] = resourcesHandles;
            }

            resourcesHandles.Add(handle);
        }
    }
}