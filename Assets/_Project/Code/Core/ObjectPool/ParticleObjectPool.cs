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
        private readonly Dictionary<int, Stack<ParticleSystem>> _particleStock;
        public ParticleObjectPool(IAssetProvider assetProvider, ParticleFactory particleFactory)
        {
            _particleStock = new Dictionary<int, Stack<ParticleSystem>>();
            _assetProvider = assetProvider;
            _particleFactory = particleFactory;
        }
        public void CleanUp()
        {
            _particleStock.Clear();
        }
        public async UniTask<ParticleSystem> GetObject(int id, Transform parent)
        {
            ParticleSystem result = null;

            if (CheckForExist(id))
            {
                result = _particleStock[id].Pop();

            }
            else
            {
                if (!_particleStock.ContainsKey(id))
                    _particleStock.Add(id, new Stack<ParticleSystem>());
                
                result = await _particleFactory.CreateParticle(id);

                var poolable = result.GetComponent<IPoolable>();
            }
            
            result.gameObject.SetActive(true);
            result.Play();
            return result;
        }


        public async UniTask<ParticleSystem> GetObject(int id)
        {
            ParticleSystem result = null;

            if (CheckForExist(id))
            {
                result = _particleStock[id].Pop();

            }
            else
            {
                if (!_particleStock.ContainsKey(id))
                    _particleStock.Add(id, new Stack<ParticleSystem>());


                result = await _particleFactory.CreateParticle(id);
                
            }

            result.gameObject.SetActive(true);
            result.Play();
            return result;
        }

        public void ReturnObject(int id, ParticleSystem particle)
        {
            _particleStock[id].Push(particle);
            
            particle.gameObject.SetActive(false);
        }
        
        
        private bool CheckForExist(int particleId)
        {
            return _particleStock.ContainsKey(particleId) && _particleStock[particleId].Count > 0;
        }
        
    }
}
