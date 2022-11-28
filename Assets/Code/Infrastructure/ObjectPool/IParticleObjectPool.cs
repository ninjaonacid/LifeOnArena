using Code.Services;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.ObjectPool
{
    public interface IParticleObjectPool : IService
    {
      public GameObject GetObject(ParticleId particleId, Transform parent);
      public void ReturnObject(ParticleId particleId, GameObject obj);
    }
}
