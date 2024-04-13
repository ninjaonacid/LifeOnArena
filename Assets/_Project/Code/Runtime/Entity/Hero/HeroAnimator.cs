using Code.Runtime.Logic.Animator;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        private static int AnimationSpeed = Animator.StringToHash("AnimationSpeed");
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _heroAnimator;

        public AnimatorState State { get; private set; }

        public void PlayAnimation(int hash)
        {
            _heroAnimator.CrossFade(hash, 0.1f);
        }

        public void PlayAnimation(int hash, float speed)
        {
            _heroAnimator.CrossFade(hash, 0.1f);
            _heroAnimator.SetFloat(AnimationSpeed, speed);
        } 

        public void OverrideController(AnimatorOverrideController controller)
        {
            _heroAnimator.runtimeAnimatorController = controller;
        }
    }
}