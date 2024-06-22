using Code.Runtime.Logic.Projectiles;
using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "ProjectileAbility", menuName = "AbilitySystem/ActiveAbility/ProjectileAbility")]
    public class ProjectileAbilityBlueprint : ActiveAbilityBlueprintBase
    {
        [SerializeField] protected Projectile _prefab;
        [SerializeField] protected float _lifeTime;
        [SerializeField] protected float _speed;
        public override ActiveAbility GetAbility()
        {
            return new ProjectileAbility(this, _prefab, _lifeTime, _speed);
        }
    }
}