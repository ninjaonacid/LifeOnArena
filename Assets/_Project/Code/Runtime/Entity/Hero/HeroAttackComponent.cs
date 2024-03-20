using System.Collections.Generic;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Factory;
using Code.Runtime.Entity.Enemy;
using Code.Runtime.Logic.Collision;
using Code.Runtime.Logic.Weapon;
using Code.Runtime.Services.BattleService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttackComponent : EntityAttack
    {
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
            _heroWeapon.OnWeaponChange += ChangeWeapon;
        }
        
        public void ClearCollisionData()
        {
            _collidedData.Clear();
        }

        private void ChangeWeapon(Weapon weapon)
        {
            weapon.SetLayerMask(_targetLayer);
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
            InvokeHit(1);
        }

        private async UniTaskVoid HitVfx(Vector3 position)
        {
            int particleId = _heroWeapon.GetEquippedWeaponData().HitVisualEffect.Identifier.Id;
            
            var particle =
                await _visualEffectFactory.CreateVisualEffect(particleId);

            particle.transform.position = position;
            particle.Play();
        }
    }
}