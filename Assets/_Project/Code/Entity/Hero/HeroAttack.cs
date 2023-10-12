using System;
using Code.ConfigData.StatSystem;
using Code.Logic.Collision;
using Code.Logic.EntitiesComponents;
using Code.Logic.Weapon;
using Code.Services.AudioService;
using Code.Services.BattleService;
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
        [SerializeField] private AudioSource _heroAudioSource;
        [SerializeField] private HeroHitBox _hitBox;
        [SerializeField] private HeroWeapon _heroWeapon;
        public CharacterController CharacterController;
        
        private AudioService _audioService;
        private IBattleService _battleService;
        
        [Inject]
        public void Construct(AudioService audioService, IBattleService battleService)
        {
            _audioService = audioService;
            _battleService = battleService;
        }
        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
            _heroWeapon.OnWeaponChange += ChangeWeapon;
        }

        private void ChangeWeapon(MeleeWeapon weapon)
        {
            weapon.Hit += BaseAttack;
        }

        private void BaseAttack(CollisionData collision)
        {
            _battleService.ApplyDamage(_stats, collision.Target);
            OnHit?.Invoke(1);
        }

        public void BaseAttack()
        {
            var hits = _battleService.CreateAttack(_stats, _hitBox.StartPoint(), _layerMask);
            
            _audioService.PlaySound3D("SwordSlash", transform, 0.5f);
            
            if (hits > 0)
            { 
                OnHit?.Invoke(hits);
                _audioService.PlaySound3D("Hit", transform, 0.5f);
            }
        }

        public void SkillAttack(Vector3 castPoint)
        {
            var hits = _battleService.CreateAttack(_stats, castPoint, _layerMask);

            if (hits > 0)
            {
                OnHit?.Invoke(hits);
            }
        }

        public void SkillAttack()
        {
        }
        
        public Vector3 StartPoint()
        {
            var position = transform.position;
            return new Vector3(position.x, CharacterController.center.y, position.z);
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(StartPoint() + transform.forward * 2, _stats.Stats["AttackRadius"].Value);
        // }
    }
}