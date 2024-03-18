using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Fireball", menuName = "AbilitySystem/Ability/Fireball")]
    public class FireballBlueprint : ActiveAbilityBlueprint<Fireball>
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;

        public override ActiveAbility GetAbility()
        {
            return new Fireball(StatusEffects, Cooldown, ActiveTime, IsCastAbility, 
                _projectile, _projectileFactory,
                _lifeTime, _speed, _projectile);
        }
    }
}