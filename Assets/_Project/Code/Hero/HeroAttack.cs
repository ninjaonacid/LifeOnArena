using Code.Data;
using Code.Logic;
using Code.Services.AudioService;
using Code.Services.BattleService;
using Code.Services.PersistentProgress;
using Code.StaticData.Ability;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, IAttack, ISaveReader
    {
        private static int _layerMask;
        private readonly Collider[] _hits = new Collider[2];

        private CharacterStats _characterStats;
        
        public HeroWeapon HeroWeapon;
        public HeroAnimator HeroAnimator;
        public CharacterController CharacterController;
        [SerializeField] private AudioSource _heroAudioSource;
        private IAudioService _audioService;
        private IBattleService _battleService;

        public void LoadProgress(PlayerProgress progress)
        {
            _characterStats = progress.CharacterStats;
        }

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
            
            if (HeroWeapon.GetEquippedWeapon() != null)
            {
                DoDamage(_characterStats.CalculateHeroDamage() +
                         HeroWeapon.GetEquippedWeapon().Damage,
                    _characterStats.CalculateHeroAttackRadius());
               // _audioService.PlayHeroAttackSound(_heroAudioSource);
            }
            else DoDamage(_characterStats.CalculateHeroDamage(), _characterStats.CalculateHeroAttackRadius());
        }

        public void SkillAttack()
        {
            if (HeroWeapon.GetEquippedWeapon() != null)
            {
                DoDamage(//_activeSkill.Damage +
                         HeroWeapon.GetEquippedWeapon().Damage,
                    HeroWeapon.GetEquippedWeapon().AttackRadius);
            }
            else DoDamage(_characterStats.CalculateHeroDamage(), _characterStats.CalculateHeroAttackRadius());
        }

        public void DoDamage(float damage, float attackRadius)
        {
            for (var i = 0; i < Hit(attackRadius); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(damage);

                Debug.Log("BaseDamage amount: " + _characterStats.CalculateHeroDamage());
                Debug.Log(_hits[i].ToString());
            }
        }

        private int Hit(float attackRadius)
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward * 2,
                _characterStats.CalculateHeroAttackRadius(),
                _hits,
                _layerMask);
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y, transform.position.z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StartPoint() + transform.forward * 2, _characterStats.CalculateHeroAttackRadius());
        }
    }
}