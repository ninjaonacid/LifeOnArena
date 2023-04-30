using System.Collections.Generic;
using Code.Infrastructure.AssetManagment;
using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public class ParticleObjectPool : IParticleObjectPool
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;
        public Dictionary<ParticleId, List<GameObject>> _particleStock;
        public ParticleObjectPool(IStaticDataService staticData, IAssetProvider assetProvider)
        {
            _particleStock = new Dictionary<ParticleId, List<GameObject>>();
            _staticData = staticData;
            _assetProvider = assetProvider;
        }
        public void CleanUp()
        {
            _particleStock.Clear();
        }
        public GameObject GetObject(ParticleId particleId, Transform parent)
        {
            GameObject result = null;
            if (CheckForExist(particleId))
            {
                result = _particleStock[particleId][0];
                _particleStock[particleId].RemoveAt(0);
            }
            else
            {
                if (!_particleStock.ContainsKey(particleId))
                    _particleStock.Add(particleId, new List<GameObject>());


                var particleData = _staticData.ForParticle(particleId);
                result = Object.Instantiate(particleData.ParticlePrefab, parent);

            }
            result.SetActive(true);
            return result;

        }

        public void ReturnObject(ParticleId particleId, GameObject obj)
        {
            _particleStock[particleId].Add(obj);
            obj.SetActive(false);
        }

        private bool CheckForExist(ParticleId particleId)
        {
            return _particleStock.ContainsKey(particleId) && _particleStock[particleId].Count > 0;

        }
    }
}
