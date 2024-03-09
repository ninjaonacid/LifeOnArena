using System;
using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Entity.EntitiesComponents;
using Code.Runtime.Entity.StatusEffects;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, IAttack
    {
        private static int _layerMask;
        public event Action<int> OnHit;
        
        [SerializeField] private StatController _stats;
        [SerializeField] private HeroHurtBox HurtBox;
        [SerializeField] private HeroWeapon _heroWeapon;

        private List<CollisionData> _collidedData;

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
        private void Awake()
        {
            _collidedData = new List<CollisionData>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
            _heroWeapon.OnWeaponChange += ChangeWeapon;
        }
        
        public void InvokeHit(int hitCount)
        {
            OnHit?.Invoke(hitCount);
        }

        public void ClearCollisionData()
        {
            _collidedData.Clear();
        }

        private void ChangeWeapon(Weapon weapon, WeaponId weaponId)
        {
            weapon.Hit += BaseAttack;
        }

        private void BaseAttack(CollisionData collision)
        {
            if(_collidedData.Contains(collision))
            {
                return;
            }

            var hitBox = collision.Target.GetComponent<EnemyHurtBox>().GetHitBoxCenter();
            
            HitVfx(hitBox + collision.Target.transform.position).Forget();

            _battleService.CreateWeaponAttack(_stats, collision.Target);
            _collidedData.Add(collision);
            OnHit?.Invoke(1);
        }

        private async UniTaskVoid HitVfx(Vector3 position)
        {
            int particleId = _heroWeapon.GetEquippedWeaponData().HitVisualEffect.Identifier.Id;
            
            var particle =
                await _visualEffectFactory.CreateVisualEffectWithTimer(particleId, 1);

            particle.transform.position = position;
            particle.Play();
        }
    }
}