using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "Fireball", menuName = "Config/AbilityData/Cast/Fireball")]
    public class FireballBlueprint : ActiveAbilityBlueprint<Fireball>
    {
        [SerializeField] private Projectile _projectile;
        public override IAbility GetAbility()
        {
            return new Fireball(_projectile, _projectileFactory);
        }
    }
}
