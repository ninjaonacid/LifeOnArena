using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private static int _layerMask;
        private readonly Collider[] _hits = new Collider[2];
        private IInputService _input;

        private Stats _stats;
        public CharacterController CharacterController;
        public HeroAnimator HeroAnimator;


        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();

            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.isAttackButtonUp() && !HeroAnimator.IsAttacking()) HeroAnimator.PlayAttack();
        }

        public void OnAttack()
        {
            for (var i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
                Debug.Log("Damage amount: " + _stats.Damage);

                Debug.Log(_hits[i].ToString());
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StartPoint() + transform.forward, _stats.DamageRadius);
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward,
                _stats.DamageRadius,
                _hits,
                _layerMask);
        }


        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y, transform.position.z);
        }
    }
}