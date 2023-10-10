using System.Collections.Generic;
using Code.Core.AssetManagement;
using Code.Core.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Core.ObjectPool
{
    public class ParticleObjectPool
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ParticleFactory _particleFactory;
        private readonly Dictionary<int, List<GameObject>> _particleStock;
        public ParticleObjectPool(IAssetProvider assetProvider, ParticleFactory particleFactory)
        {
            _particleStock = new Dictionary<int, List<GameObject>>();
            _assetProvider = assetProvider;
            _particleFactory = particleFactory;
        }
        public void CleanUp()
        {
            _particleStock.Clear();
        }
        public async UniTask<GameObject> GetObject(int id, Transform parent)
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
        

        public async UniTask<GameObject> GetObject(int id)
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

        public void ReturnObject(int id, GameObject obj)
        {
            _particleStock[id].Add(obj);
            obj.SetActive(false);
        }
        
     

        private bool CheckForExist(int particleId)
        {
            return _particleStock.ContainsKey(particleId) && _particleStock[particleId].Count > 0;
        }
        
    }
}
