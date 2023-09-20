using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Core.ObjectPool
{
    public interface IParticleObjectPool : IService
    {
      public UniTask<GameObject> GetObject(AssetReference particleReference, Transform parent);
      public UniTask<GameObject> GetObject(AssetReference particleReference);
      public void ReturnObject(AssetReference particle, GameObject obj);
      void CleanUp();
    }
}
