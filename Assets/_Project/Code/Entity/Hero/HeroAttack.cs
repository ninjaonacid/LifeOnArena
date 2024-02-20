using System;
using System.Collections.Generic;
using Code.ConfigData.Identifiers;
using Code.ConfigData.StatSystem;
using Code.Core.Audio;
using Code.Core.Factory;
using Code.Entity.Enemy;
using Code.Entity.EntitiesComponents;
using Code.Entity.StatusEffects;
using Code.Logic.Collision;
using Code.Logic.Weapon;
using Code.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
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
        private ParticleFactory _particleFactory;

        [Inject]
        public void Construct(AudioService audioService, BattleService battleService, ParticleFactory particleFactory)
        {
            _audioService = audioService;
            _battleService = battleService;
            _particleFactory = particleFactory;
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
        

        public void AoeAbilityAttack(Vector3 castPoint, IReadOnlyList<StatusEffect> statusEffects)
        {
            // var hits = _battleService.CreateAoeAbility(_stats, statusEffects,  castPoint, _layerMask);
            //
            // if (hits > 0)
            // {
            //     OnHit?.Invoke(hits);
            // }
        }

        public void ClearCollisionData()
        {
            _collidedData.Clear();
        }

        private void ChangeWeapon(MeleeWeapon weapon, WeaponId weaponId)
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
            int particleId = _heroWeapon.GetEquippedWeaponData().HitVfx.Identifier.Id;
            
            var particle =
                await _particleFactory.CreateParticleWithTimer(particleId, 1);

            particle.transform.position = position;
            particle.Play();
        }
    }
}