using Code.Services;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.ObjectPool
{
    public interface IParticleObjectPool : IService
    {
      public UniTask<GameObject> GetObject(AssetReference particleReference, Transform parent);
      public void ReturnObject(AssetReference particle, GameObject obj);
      void CleanUp();
    }
}
