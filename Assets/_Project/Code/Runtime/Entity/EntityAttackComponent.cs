using System;
using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity
{
    public class EntityAttackComponent : MonoBehaviour, IAttackComponent
    {
        public event Action<int> OnHit;
        
        [SerializeField] private LayerMask _ownerLayer;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected StatController _stats;
        [SerializeField] protected EntityWeapon _entityWeapon;
        [SerializeField] protected VisualEffectController _visualEffectController;
        
        private Dictionary<CollisionData, int> _collidedData;

        private AudioService _audioService;
        private BattleService _battleService;

        [Inject]
        public void Construct(AudioService audioService, BattleService battleService)
        {
            _audioService = audioService;
            _battleService = battleService;
        }
        
        public LayerMask GetTargetLayer()
        {
            return _targetLayer;
        }

        public LayerMask GetLayer()
        {
            return _entityWeapon.MainWeaponView.gameObject.layer;
        }
        
        public void InvokeHit(int hitCount)
        {
            OnHit?.Invoke(hitCount);
        }
        
        public void ClearCollisionData()
        {
            _collidedData.Clear();
        }

        public float GetAttacksPerSecond()
        {
            float attackSpeed = _stats.Stats["AttackSpeed"].Value;
            
            float attacksPerSecond = attackSpeed * 0.2f;

            return attacksPerSecond;
        }

        private void Awake()
        {
            _collidedData = new Dictionary<CollisionData, int>();
            _entityWeapon.OnWeaponChange += ChangeWeapon;
        }

        private void ChangeWeapon(WeaponView weaponView)
        {
            weaponView.SetLayerMask(_targetLayer);
            weaponView.gameObject.layer = Mathf.RoundToInt(Mathf.Log(_ownerLayer.value, 2));
            weaponView.Hit += BaseAttack;
        }

        protected virtual void BaseAttack(CollisionData collision)
        {
            if (_collidedData.ContainsKey(collision))
            {
                if (_collidedData[collision] < _entityWeapon.WeaponData.PossibleCollisions)
                {
                    _collidedData[collision]++;
                }
                else
                {
                    return;
                }
            }
            else
            {
                _collidedData.Add(collision, 1);
            }
            
            var hitBox = collision.Target.GetComponentInParent<EntityHurtBox>().GetHitBoxCenter();

            _visualEffectController.PlayVisualEffect(
                _entityWeapon.WeaponData.HitVisualEffect, collision.Target.transform.position).Forget();

            _audioService.PlaySound3D(_entityWeapon.WeaponData.WeaponHitSound, collision.Target.transform, 1f);
            _battleService.CreateWeaponAttack(_stats, collision.Target);

            InvokeHit(1);
        }
    }
}