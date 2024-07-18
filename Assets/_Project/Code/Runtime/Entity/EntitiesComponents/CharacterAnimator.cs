using Code.Runtime.Logic.Animator;
using UnityEngine;

namespace Code.Runtime.Entity.EntitiesComponents
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        private static int AnimationSpeed = Animator.StringToHash("AnimationSpeed");
        private static int AttackSpeed = Animator.StringToHash("AttackSpeed");
        
        
        [SerializeField] private Animator _characterAnimator;

        public AnimatorState State { get; private set; }

        public void PlayAnimation(int hash)
        {
            _characterAnimator.CrossFade(hash, 0.1f);
        }

        public void PlayAnimation(string animationName)
        {
            _characterAnimator.CrossFade(animationName, 0.1f);
        }

        public void PlayAnimation(int hash, float speed)
        {
            _characterAnimator.CrossFade(hash, 0.1f);
            _characterAnimator.SetFloat(AnimationSpeed, speed);
        } 
        
        
        public void SetAttackAnimationSpeed(float value)
        {
            _characterAnimator.SetFloat(AttackSpeed,  value);
        }


        public void OverrideController(AnimatorOverrideController controller)
        {
            _characterAnimator.runtimeAnimatorController = controller;
        }
    }
}