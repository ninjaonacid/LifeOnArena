using System;
using System.Collections.Generic;
using Code.ConfigData.Identifiers;
using Code.ConfigData.StatSystem;
using Code.Core.Factory;
using Code.Entity.Enemy;
using Code.Entity.EntitiesComponents;
using Code.Logic.Collision;
using Code.Logic.EntitiesComponents;
using Code.Logic.Weapon;
using Code.Services.AudioService;
using Code.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace Code.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, IAttack
    {
        private static int _layerMask;
        public event Action<int> OnHit;
        
        [SerializeField] private StatController _stats;
        [FormerlySerializedAs("_hitBox")] [SerializeField] private HeroHurtBox HurtBox;
        [SerializeField] private HeroWeapon _heroWeapon;

        private List<CollisionData> _collidedData;

        private AudioService _audioService;
        private IBattleService _battleService;
        private ParticleFactory _particleFactory;

        [Inject]
        public void Construct(AudioService audioService, IBattleService battleService, ParticleFactory particleFactory)
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

            _battleService.ApplyDamage(_stats, collision.Target);
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

        public void ClearCollisionData()
        {
            _collidedData.Clear();
        }

        public void BaseAttack()
        {
            var hits = _battleService.CreateAoeAttack(_stats, HurtBox.StartPoint(), _layerMask);
            
            _audioService.PlaySound3D("SwordSlash", transform, 0.5f);
            
            if (hits > 0)
            { 
                OnHit?.Invoke(hits);
                _audioService.PlaySound3D("Hit", transform, 0.5f);
            }
        }

        public void SkillAttack(Vector3 castPoint)
        {
            var hits = _battleService.CreateAoeAttack(_stats, castPoint, _layerMask);

            if (hits > 0)
            {
                OnHit?.Invoke(hits);
            }
        }
        
        
    }
}