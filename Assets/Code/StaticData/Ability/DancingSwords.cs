using Code.Hero;
using Code.Logic.Abilities;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.StaticData.Ability
{
    public class DancingSwords : IAbility
    {
        private readonly SpinningProjectile _prefab;
        public DancingSwords(SpinningProjectile prefab)
        {
            _prefab = prefab;
        }


        public void Use(GameObject target, GameObject caster)
        {
            var pointOfRotation = caster.GetComponentInChildren<PointOfRotation>();

            SpinningProjectile projectile = Object.Instantiate
                (
                    _prefab, 
                    caster.transform.position + new Vector3(2, 0, 0), 
                    new Quaternion(0.7f, 0.7f, 0, 0), 
                    pointOfRotation.transform);

            projectile.SetProjectile(pointOfRotation.transform);

        }

        
    }
}
