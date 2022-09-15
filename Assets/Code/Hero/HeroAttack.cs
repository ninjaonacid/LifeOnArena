using Code.Data;
using Code.Logic;
using Code.Services.PersistentProgress;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        private static int _layerMask;
        private readonly Collider[] _hits = new Collider[2];
        private CharacterStats _characterStats;
  
        public CharacterController CharacterController;
        public HeroAnimator HeroAnimator;

        public void LoadProgress(PlayerProgress progress)
        {
            _characterStats = progress.CharacterStats;
        }

        private void Awake()
        {
            
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }


        /// <summary>
        ///  AnimationEvent
        /// </summary>
        public void OnAttack()
        {
            for (var i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_characterStats.BaseDamage);
                Debug.Log("BaseDamage amount: " + _characterStats.BaseDamage);

                Debug.Log(_hits[i].ToString());
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
           // Gizmos.DrawWireSphere(StartPoint() + transform.forward, _characterStats.BaseAttackRadius);
        }

        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward,
                _characterStats.BaseAttackRadius ,
                _hits,
                _layerMask);
        }


        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, CharacterController.center.y, transform.position.z);
        }

   
    }
}