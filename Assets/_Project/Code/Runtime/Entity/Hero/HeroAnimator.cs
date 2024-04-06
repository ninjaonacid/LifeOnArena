using System;
using Code.Runtime.Entity.Hero.HeroStates;
using Code.Runtime.Logic.Animator;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _heroAnimator;

        public AnimatorState State { get; private set; }

        public void PlayAnimation(int hash)
        {
            _heroAnimator.CrossFade(hash, 0.1f);
        }
        
        public void OverrideController(AnimatorOverrideController controller)
        {
            _heroAnimator.runtimeAnimatorController = controller;
        }
        
    }
}