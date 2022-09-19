using System;
using Code.Services;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class HeroInput : MonoBehaviour
    {
        private HeroStateMachine heroStateMachine;
        private IInputService _inputService;
        private Func<bool> AttackButtonPressed;
        private void Awake()
        {
            heroStateMachine = GetComponent<HeroStateMachine>();
            _inputService = AllServices.Container.Single<IInputService>();

        }

        private void Update()
        {
            
        }
    }
}
