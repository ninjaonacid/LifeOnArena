using System.Collections.Generic;
using Code.Services;
using Code.StaticData;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public class ParticleObjectPool : IParticleObjectPool
    {
        private readonly IStaticDataService _staticData;
        public Dictionary<ParticleId, List<GameObject>> _particleStock;
        public ParticleObjectPool(IStaticDataService staticData)
        {
            _particleStock = new Dictionary<ParticleId, List<GameObject>>();
            _staticData = staticData;
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

            return result;

        }

        public void ReturnObject(ParticleId particleId, GameObject obj)
        {
            _particleStock[particleId].Add(obj);
        }

        private bool CheckForExist(ParticleId particleId)
        {
            return _particleStock.ContainsKey(particleId) && _particleStock[particleId].Count > 4;

        }
    }
}
