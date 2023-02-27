using System.Threading.Tasks;
using Code.Hero;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IHeroFactory : IService
    {
        GameObject HeroGameObject { get; set; }
        Task<GameObject> CreateHero(Vector3 initialPoint);
        void InitAssets();
    }
}
