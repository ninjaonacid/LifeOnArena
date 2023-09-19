using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IHeroFactory : IService
    {
        GameObject HeroGameObject { get; set; }
        UniTask<GameObject> CreateHero(Vector3 initialPoint);
        UniTask InitAssets();
        UniTask<GameObject> CreateHeroUnregistered(Vector3 initialPoint, Quaternion rotation);
    }
}
