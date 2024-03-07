using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Fireball", menuName = "AbilitySystem/Ability/Fireball")]
    public class FireballBlueprint : ActiveAbilityBlueprint<Fireball>
    {
        [SerializeField] private Projectile _projectile;
        public override IAbility GetAbility()
        {
            return new Fireball(_projectile, _projectileFactory);
        }
    }
}
