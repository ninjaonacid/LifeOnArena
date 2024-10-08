using Code.Runtime.Data.PlayerData;
using Code.Runtime.Entity.Hero;
using Code.Runtime.Logic.Projectiles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Runtime.Modules.AbilitySystem.PassiveAbilities
{
    public class DancingSwords : IPassiveAbility
    {
        private readonly SpinningProjectile _prefab;
        public DancingSwords(SpinningProjectile prefab)
        {
            _prefab = prefab;
        }

        public void Apply(GameObject hero, PlayerData heroData)
        {
            var pointOfRotation = hero.GetComponentInChildren<PointOfRotation>();

            SpinningProjectile projectile = Object.Instantiate
            (
                _prefab,
                hero.transform.position + new Vector3(2, 0, 0),
                new Quaternion(0.7f, 0.7f, 0, 0),
                pointOfRotation.transform);

            projectile.SetProjectile(pointOfRotation.transform);
        }
    }
}
