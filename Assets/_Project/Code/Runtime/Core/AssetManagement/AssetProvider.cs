using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Runtime.Core.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();
    
        private readonly Dictionary<string, List<AsyncOperationHandle>> _loadingAssets = new();
        
        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async UniTask<T> Load<T>(AssetReferenceGameObject assetReference, CancellationToken cancellationToken = default) where T : class
        {
            if (_loadedAssets.TryGetValue(assetReference.AssetGUID, 
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            cancellationToken.ThrowIfCancellationRequested();

            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(assetReference),
                assetReference.AssetGUID);
        }

        public async UniTask<T> Load<T>(AssetReferenceSprite spriteReference, CancellationToken cancellationToken = default) where T : class
        {
            if (_loadedAssets.TryGetValue(spriteReference.AssetGUID,
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            cancellationToken.ThrowIfCancellationRequested();
          
            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(spriteReference),
                spriteReference.AssetGUID);
        }

        public async UniTask<T> Load<T>(AssetReference assetReference, CancellationToken cancellationToken = default) where T : class
        {
            if (_loadedAssets.TryGetValue(assetReference.AssetGUID,
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            cancellationToken.ThrowIfCancellationRequested();
            
            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(assetReference),
                assetReference.AssetGUID);
        }

        
        public async UniTask<T> Load<T>(string assetAddress, CancellationToken cancellationToken = default) where T : class
        {
            if (_loadedAssets.TryGetValue(assetAddress,
                    out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            cancellationToken.ThrowIfCancellationRequested();
            
            return await LoadWithCache(
                Addressables.LoadAssetAsync<T>(assetAddress),
                assetAddress);
        }

        public void Unload(AssetReference assetReference) 
        {
            if(_loadedAssets.TryGetValue(assetReference.AssetGUID, out var handle))
            {
                _loadedAssets.Remove(assetReference.AssetGUID);
                Addressables.Release(handle);
            }
        }
        
        
        public GameObject InstantiateSync(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject InstantiateSync(string path, Vector3 point)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, point, Quaternion.identity);
        }

        public GameObject InstantiateSync(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }

        public void UnloadAll()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _loadingAssets.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }

            _loadedAssets.Clear();
            _loadingAssets.Clear();
        }

        private async UniTask<T> LoadWithCache<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += operation => { _loadedAssets[cacheKey] = operation; };

            AddHandleOperation(handle, cacheKey);

            return await handle.Task;
        }

        private void AddHandleOperation<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            if (!_loadingAssets.TryGetValue(cacheKey, out List<AsyncOperationHandle> resourcesHandles))
            {
                resourcesHandles = new List<AsyncOperationHandle>();
                _loadingAssets[cacheKey] = resourcesHandles;
            }

            resourcesHandles.Add(handle);
        }
    }
}