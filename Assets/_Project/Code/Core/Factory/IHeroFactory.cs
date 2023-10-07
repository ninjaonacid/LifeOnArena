using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Core.Factory
{
    public interface IHeroFactory : IService
    {
        GameObject HeroGameObject { get; set; }
        UniTask<GameObject> CreateHero(Vector3 initialPoint);
        UniTaskVoid InitAssets();
        UniTask<GameObject> CreateHeroUnregistered(Vector3 initialPoint, Quaternion rotation);
        UniTask<GameObject> CreateHero(Vector3 initialPoint,  Quaternion rotation);
    }
}
