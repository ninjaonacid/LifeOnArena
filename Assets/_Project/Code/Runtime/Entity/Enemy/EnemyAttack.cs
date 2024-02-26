using System.Linq;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.BattleService;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private StatController _stats;
        private float _attackCooldown;
        private bool _attackIsActive;
        public bool TargetInAttackRange => _attackIsActive;
        private readonly Collider[] _hits = new Collider[1];

        private bool _isAttacking;

        private int _layerMask;

        private BattleService _battleService;
        
        [Inject]
        public void Construct(BattleService battleService)
        {
            _battleService = battleService;
        }
        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("PlayerHitBox");
        }

        private void OnDisable()
        {
            _attackIsActive = false;
        }

        private void OnEnable()
        {
            _isAttacking = false;
        }

        private void Update()
        {
            UpdateCooldown();
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }


        public void DisableAttack()
        {
            _attackIsActive = false;
        }
        

        private void UpdateCooldown()
        {
            if (!CoolDownIsUp())
                _attackCooldown -= Time.deltaTime;
        }

        public void AttackEnded()
        {
            _attackCooldown = _stats.Stats["AttackSpeed"].Value;
            _isAttacking = false;
        }

        public bool CanAttack()
        {
            return CoolDownIsUp() && !_isAttacking && _attackIsActive;
        }

        public void Attack()
        {
            _isAttacking = true;
            _battleService.CreateOverlapAttack(_stats, StartPoint(), _layerMask);
        }

        private bool Hit(out Collider hit)
        {
            var startPoint = StartPoint();

            var hitscount = Physics.OverlapSphereNonAlloc(startPoint,
                _stats.Stats["AttackRadius"].Value,
                _hits,
                _layerMask);
            hit = _hits.FirstOrDefault();
            return hitscount > 0;
        }


        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x,
                transform.position.y + 2f,
                transform.position.z) + transform.forward * _stats.Stats["AttackDistance"].Value;
        }

        private bool CoolDownIsUp()
        {
            return _attackCooldown <= 0f;
        }
    }
}