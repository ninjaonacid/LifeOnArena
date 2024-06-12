using System.Threading;
using UnityEngine;

namespace Code.Runtime.Entity.Enemy
{
    public class EnemyHurtBox : EntityHurtBox
    {
        private bool _isInvincible = false;
        private LayerMask _playerWeaponLayer;
        private float _hitBoxTimer;
        private readonly float _hitBoxCooldown = 0.5f;
        private CancellationTokenSource _cts;
        private Vector3 _boxColliderSize;

        [SerializeField] private TriggerObserver _observer;
        
        public Vector3 LastHitPosition { get; set; }
        private void Awake()
        {
            _hitBoxTimer = _hitBoxCooldown;
            _boxColliderSize = _hitBoxCollider.size;
            _playerWeaponLayer = LayerMask.NameToLayer("PlayerWeapon");

            _observer.TriggerEnter += TriggerEnter;
        }
        
        

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.gameObject.layer == _playerWeaponLayer.value && !_isInvincible)
        //     {
        //         _isInvincible = true;
        //         _hitBoxCollider.size = new Vector3(0, 0, 0);
        //     }
        // }
        
        private void TriggerEnter(Collider other)
        {
            LastHitPosition = other.gameObject.transform.position;
        }

        // private void Update()
        // {
        //     _hitBoxTimer -= Time.deltaTime;
        //     if (_hitBoxTimer < 0 && _isInvincible)
        //     {
        //         _isInvincible = false;
        //         _hitBoxTimer = _hitBoxCooldown;
        //         _hitBoxCollider.size = _boxColliderSize;
        //     }
        // }
    }
}
