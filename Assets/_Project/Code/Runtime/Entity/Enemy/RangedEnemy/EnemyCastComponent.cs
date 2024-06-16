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

        private float _currentCooldown;

        private bool _isCastCooldown;
        private bool _targetInAttackRange;
        public bool TargetInAttackRange => _targetInAttackRange;


        private void Update()
        {
            if (_isCastCooldown)
            {
                _currentCooldown -= Time.deltaTime;
                if (_currentCooldown <= 0)
                {
                    _isCastCooldown = false;
                }
            }
        }

        public void Cast()
        {
            _abilityController.TryActivateAbility(_rangeAbility);
            _currentCooldown = _castCooldown;
            _isCastCooldown = true;

        }
        public bool CanAttack()
        {
            return  !_isCastCooldown && _abilityController.CanActivateAbility(_rangeAbility);
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