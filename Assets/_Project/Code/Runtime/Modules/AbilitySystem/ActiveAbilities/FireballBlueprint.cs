using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Fireball", menuName = "AbilitySystem/Ability/Fireball")]
    public class FireballBlueprint : ActiveAbilityBlueprint<ProjectileAbility>
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        public override ActiveAbility GetAbility()
        {
            return new ProjectileAbility(this, _projectile, _lifeTime, _speed);
        }
    }
}