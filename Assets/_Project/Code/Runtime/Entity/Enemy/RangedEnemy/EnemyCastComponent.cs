using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Modules.AbilitySystem;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy.RangedEnemy
{
    public class EnemyCastComponent : MonoBehaviour
    {
        [SerializeField] private AbilityController _abilityController;
        [SerializeField] private AbilityIdentifier _rangeAbility;
        [SerializeField] private float _castCooldown;

        private bool _isCastCooldown;
        private bool _targetInAttackRange;
        public bool TargetInAttackRange => _targetInAttackRange;


        private void Update()
        {
            if (_isCastCooldown)
            {
                _castCooldown -= Time.deltaTime;
            }
        }
        public bool CanAttack()
        {
            return  !_isCastCooldown && _abilityController.TryActivateAbility(_rangeAbility);
        }

        public void EnableCast()
        {
            _targetInAttackRange = true;
        }

        public void DisableCast()
        {
            _targetInAttackRange = false;
        }
    }
}