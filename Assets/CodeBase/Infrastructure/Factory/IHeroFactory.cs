using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IHeroFactory : IFactory
    {
        GameObject HeroGameObject { get; set; }
        GameObject CreateHero(GameObject initialPoint);
    }
}
