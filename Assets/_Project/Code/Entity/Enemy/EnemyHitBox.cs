using System.Collections;
using System.Threading;
using UnityEngine;
using Timer = Code.Logic.Timer.Timer;

namespace Code.Entity.Enemy
{
    public class EnemyHitBox : EntityHitBox
    {
        private bool _isInvincible = false;
        private LayerMask _playerWeaponLayer;
        private Timer _hitBoxTimer;
        private float _hitBoxCooldown = 0.5f;
        private CancellationTokenSource _cts;
        private Vector3 _boxColliderSize;
        private void Awake()
        {
            _hitBoxTimer = new Timer();
            _boxColliderSize = _hitBoxCollider.size;
            _playerWeaponLayer = LayerMask.NameToLayer("PlayerWeapon");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _playerWeaponLayer.value && !_isInvincible)
            {
                _isInvincible = true;
                _hitBoxCollider.size = new Vector3(0, 0, 0);
                _hitBoxTimer.Reset();
                StartCoroutine(Cooldown());
            }
        }


        private IEnumerator Cooldown()
        {
            while (_hitBoxTimer.Elapsed < _hitBoxCooldown)
            {
                yield return null;
            }
            _hitBoxCollider.size = _boxColliderSize;
            _isInvincible = false;
            
            
        }
    }
}
