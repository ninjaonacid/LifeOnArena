using System;
using Code.ConfigData.StatSystem;
using Code.Logic.EntitiesComponents;
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
        }


        public void BaseAttack()
        {
            var hits = _battleService.CreateAttack(_stats, StartPoint(), _layerMask);
            if (hits > 0)
            { 
                OnHit?.Invoke(hits);
                _audioService.PlaySound3D("Hit", transform, 1f);
            }
            
        }

        public void SkillAttack()
        {
        }
        

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y, transform.position.z);
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(StartPoint() + transform.forward * 2, _stats.Stats["AttackRadius"].Value);
        // }
    }
}