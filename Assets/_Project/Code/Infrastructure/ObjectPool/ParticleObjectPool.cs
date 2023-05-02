using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.ObjectPool
{
    public class ParticleObjectPool : IParticleObjectPool
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<AssetReference, List<GameObject>> _particleStock;
        public ParticleObjectPool(IAssetProvider assetProvider)
        {
            _particleStock = new Dictionary<AssetReference, List<GameObject>>();
            _assetProvider = assetProvider;
        }
        public void CleanUp()
        {
            _particleStock.Clear();
        }
        public async UniTask<GameObject> GetObject(AssetReference particleReference, Transform parent)
        {
            GameObject result = null;

            if (CheckForExist(particleReference))
            {
                result = _particleStock[particleReference][0];
                _particleStock[particleReference].RemoveAt(0);
            }
            else
            {
                if (!_particleStock.ContainsKey(particleReference))
                    _particleStock.Add(particleReference, new List<GameObject>());


                var particle = await _assetProvider.Load<GameObject>(particleReference);
                result = Object.Instantiate(particle, parent);
            }

            result.SetActive(true);
            return result;
        }

        public async UniTask<GameObject> GetObject(AssetReference particleReference)
        {
            GameObject result = null;

            if (CheckForExist(particleReference))
            {
                result = _particleStock[particleReference][0];
                _particleStock[particleReference].RemoveAt(0);
            }
            else
            {
                if (!_particleStock.ContainsKey(particleReference))
                    _particleStock.Add(particleReference, new List<GameObject>());


                var particle = await _assetProvider.Load<GameObject>(particleReference);
                result = Object.Instantiate(particle);
            }

            result.SetActive(true);
            return result;
        }

        public void ReturnObject(AssetReference particle, GameObject obj)
        {
            _particleStock[particle].Add(obj);
            obj.SetActive(false);
        }

        private bool CheckForExist(AssetReference particleId)
        {
            return _particleStock.ContainsKey(particleId) && _particleStock[particleId].Count > 0;
        }


        //public GameObject GetObject(ParticleId particleReference, Transform parent)
        //{
        //    GameObject result = null;
        //    if (CheckForExist(particleReference))
        //    {
        //        result = _particleStock[particleReference][0];
        //        _particleStock[particleReference].RemoveAt(0);
        //    }
        //    else
        //    {
        //        if (!_particleStock.ContainsKey(particleReference))
        //            _particleStock.Add(particleReference, new List<GameObject>());


        //        var particleData = _staticData.ForParticle(particleReference);
        //        result = Object.Instantiate(particleData.ParticlePrefab, parent);

        //    }
        //    result.SetActive(true);
        //    return result;

        //}
    }
}
