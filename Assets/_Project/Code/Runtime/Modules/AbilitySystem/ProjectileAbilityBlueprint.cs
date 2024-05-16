using Code.Runtime.Logic.Projectiles;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "ProjectileAbility", menuName = "AbilitySystem/ActiveAbility/ProjectileAbility")]
    public class ProjectileAbilityBlueprint : ActiveAbilityBlueprintBase
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        public override ActiveAbility GetAbility()
        {
            return new ProjectileAbility(this, _prefab, _lifeTime, _speed);
        }
    }
}