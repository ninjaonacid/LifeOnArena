using System.Collections.Generic;
using Code.ConfigData.Identifiers;
using Code.Core.AssetManagement;
using Code.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.ObjectPool
{
    public class ParticleObjectPool
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ParticleFactory _particleFactory;
        private readonly Dictionary<Identifier, List<GameObject>> _particleStock;
        public ParticleObjectPool(IAssetProvider assetProvider, ParticleFactory particleFactory)
        {
            _particleStock = new Dictionary<Identifier, List<GameObject>>();
            _assetProvider = assetProvider;
            _particleFactory = particleFactory;
        }
        public void CleanUp()
        {
            _particleStock.Clear();
        }
        public async UniTask<GameObject> GetObject(Identifier id, Transform parent)
        {
            GameObject result = null;

            if (CheckForExist(id))
            {
                result = _particleStock[id][0];
                _particleStock[id].RemoveAt(0);
            }
            else
            {
                if (!_particleStock.ContainsKey(id))
                    _particleStock.Add(id, new List<GameObject>());


                var particle = await _particleFactory.CreateParticle(id);

                var poolable = particle.GetComponent<IPoolable>();

                result = Object.Instantiate(particle, parent);
            }

            result.SetActive(true);
            return result;
        }
        

        public async UniTask<GameObject> GetObject(Identifier id)
        {
            GameObject result = null;

            if (CheckForExist(id))
            {
                result = _particleStock[id][0];
                _particleStock[id].RemoveAt(0);
            }
            else
            {
                if (!_particleStock.ContainsKey(id))
                    _particleStock.Add(id, new List<GameObject>());


                var particle = await _particleFactory.CreateParticle(id);
                result = Object.Instantiate(particle);
            }

            result.SetActive(true);
            return result;
        }

        public void ReturnObject(Identifier id, GameObject obj)
        {
            _particleStock[id].Add(obj);
            obj.SetActive(false);
        }
        
     

        private bool CheckForExist(Identifier particleId)
        {
            return _particleStock.ContainsKey(particleId) && _particleStock[particleId].Count > 0;
        }
        
    }
}
