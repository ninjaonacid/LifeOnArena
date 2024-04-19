using System;
using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
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
        protected List<CollisionData> _collidedData;

        [SerializeField] private LayerMask _ownerLayer;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected StatController _stats;
        [SerializeField] protected EntityWeapon _entityWeapon;

        private AudioService _audioService;
        private BattleService _battleService;
        private VisualEffectFactory _visualEffectFactory;

        [Inject]
        public void Construct(AudioService audioService, BattleService battleService, VisualEffectFactory visualEffectFactory)
        {
            _audioService = audioService;
            _battleService = battleService;
            _visualEffectFactory = visualEffectFactory;
        }
        
        public LayerMask GetTargetLayer()
        {
            return _targetLayer;
        }

        public LayerMask GetLayer()
        {
            return _entityWeapon.GetEquippedWeapon().gameObject.layer;
        }
        
        public void InvokeHit(int hitCount)
        {
            OnHit?.Invoke(hitCount);
        }
        
        public void ClearCollisionData()
        {
            _collidedData.Clear();
        }
        
        private void Awake()
        {
            _collidedData = new List<CollisionData>();
            _entityWeapon.OnWeaponChange += ChangeWeapon;
        }
        
        private void ChangeWeapon(WeaponView weaponView)
        {
            weaponView.SetLayerMask(_targetLayer);
            weaponView.gameObject.layer = Mathf.RoundToInt(Mathf.Log(_ownerLayer.value, 2));
            weaponView.Hit += BaseAttack;
        }
        
        private void BaseAttack(CollisionData collision)
        {
            if(_collidedData.Contains(collision))
            {
                return;
            }

            var hitBox = collision.Target.GetComponentInParent<EntityHurtBox>().GetHitBoxCenter();
            
            HitVfx(hitBox + collision.Target.transform.position).Forget();
            
            _audioService.PlaySound3D("Hit", collision.Target.transform, 1f);
            _battleService.CreateWeaponAttack(_stats, collision.Target);
            _collidedData.Add(collision);
            InvokeHit(1);
        }

        private async UniTaskVoid HitVfx(Vector3 position)
        {
            int particleId = _entityWeapon.GetEquippedWeaponData().HitVisualEffect.Identifier.Id;
            
            var particle =
                await _visualEffectFactory.CreateVisualEffect(particleId);

            particle.transform.position = position;
            particle.Play();
        }

        public float GetAttacksPerSecond()
        {
            float attackSpeed = _stats.Stats["AttackSpeed"].Value;
            
            if (attackSpeed >= 0) {
             
                float bias = 1.25f - attackSpeed / 8.0f;
                
                bias = Mathf.Clamp(bias, 0, 1);

                return attackSpeed + bias;
            }

            return Mathf.Abs(1.0f / (1.0f - attackSpeed));
        }
    }
}