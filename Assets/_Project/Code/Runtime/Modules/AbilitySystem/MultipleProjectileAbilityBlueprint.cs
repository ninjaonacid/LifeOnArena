using Code.Runtime.Modules.AbilitySystem.ActiveAbilities;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [CreateAssetMenu(fileName = "MultipleProjectileAbility", menuName = "AbilitySystem/ActiveAbility/MultipleProjectileAbility")]
    public class MultipleProjectileAbilityBlueprint : ProjectileAbilityBlueprint
    {
        [SerializeField] private int _numberOfProjectiles;

        public override ActiveAbility GetAbility()
        {
            return new MultipleProjectileAbility(this, _prefab, _lifeTime, _speed, _numberOfProjectiles);
        }
    }
}