using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Core.Factory
{
    public interface IHeroFactory
    {
        GameObject HeroGameObject { get; set; }
        UniTask<GameObject> CreateHero(Vector3 initialPoint);
        UniTaskVoid InitAssets();
        UniTask<GameObject> CreateHeroUnregistered(Vector3 initialPoint, Quaternion rotation);
        UniTask<GameObject> CreateHero(Vector3 initialPoint,  Quaternion rotation);
    }
}
