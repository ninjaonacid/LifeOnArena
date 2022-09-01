using Code.Hero;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
    public interface IHeroFactory : IService
    {
        GameObject HeroGameObject { get; set; }
        GameObject CreateHero(Vector3 initialPoint);

    }
}
