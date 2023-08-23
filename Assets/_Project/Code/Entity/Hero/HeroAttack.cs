using Code.Logic.EntitiesComponents;
using Code.Services.AudioService;
using Code.Services.BattleService;
using Code.StaticData.StatSystem;
using UnityEngine;
using VContainer;

namespace Code.Entity.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, IAttack
    {
        private static int _layerMask;
        private readonly Collider[] _hits = new Collider[2];
        
        [SerializeField] private StatController _stats;
        [SerializeField] private AudioSource _heroAudioSource;
        
        public CharacterController CharacterController;
        
        private IAudioService _audioService;
        private IBattleService _battleService;
        
        [Inject]
        public void Construct(IAudioService audioService, IBattleService battleService)
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
            _battleService.CreateAttack(_stats, StartPoint(), _layerMask);
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