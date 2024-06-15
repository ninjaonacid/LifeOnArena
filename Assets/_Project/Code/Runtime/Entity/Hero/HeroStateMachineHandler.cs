using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.Config;
using Code.Runtime.Entity.Hero.HeroStates;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Animator;
using Code.Runtime.Modules.AbilitySystem;
using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StateMachine.States;
using Code.Runtime.Modules.StateMachine.Transitions;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroStateMachineHandler : MonoBehaviour
    {
        private ConfigProvider _configProvider;

        private const string HeroIdle = "HeroIdle";
        private const string HeroMovement = "HeroMovement";
        private const string AbilityCast = "AbilityCast";
        private const string SpinAttackAbility = "SpinAttackAbility";
        private const string RollAbility = "RollAbility";
        private const string HeroBaseAttack1 = "HeroBaseAttack1";
        private const string HeroBaseAttack2 = "HeroBaseAttack2";
        private const string HeroBaseAttack3 = "HeroBaseAttack3";

        private FiniteStateMachine _stateMachine;
        private PlayerControls _controls;
        private AudioService _audioService;

        [SerializeField] private AbilityIdentifier _spinAttackAbilityId;
        [SerializeField] private AbilityIdentifier _stompAbilityId;
        [SerializeField] private AbilityIdentifier _commonAttackId;

        [SerializeField] private AnimationEventReceiver _eventReceiver;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroRotation _heroRotation;
        [SerializeField] private HeroAttackComponent _heroAttackComponent;
        [SerializeField] private HeroAbilityController _heroAbilityController;
        [SerializeField] private HeroWeapon _heroWeapon;
        [SerializeField] private TagController _tagController;
        [SerializeField] private AnimationDataContainer _animationData;
        [SerializeField] private HeroDeath _heroDeath;
        [SerializeField] private VisualEffectController _vfxController;


        [Inject]
        public void Construct(PlayerControls controls, AudioService audioService, ConfigProvider configProvider)
        {
            _controls = controls;
            _audioService = audioService;
            _configProvider = configProvider;
        }

        void Update()
        {
            _stateMachine.OnLogic();
        }

        void Start()
        {
            _stateMachine = new FiniteStateMachine();

            _heroAbilityController.OnAbilityUse += HandleAbilityUse;

            _stateMachine.AddState(HeroIdle, new HeroIdleState(
                _heroAnimator, _heroMovement, _heroRotation, _animationData, false, true));

            _stateMachine.AddState(HeroMovement, new HeroMovementState(
                _heroAnimator, _heroMovement, _heroRotation, _animationData, false, false));

            _stateMachine.AddState(nameof(HeroDeathState), new HeroDeathState(
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData));

            _stateMachine.AddState(RollAbility, new RollDodgeState(
                _heroWeapon, _heroAbilityController, _heroAnimator, _heroMovement, _heroRotation, _animationData, true,
                false));

            _stateMachine.AddState(SpinAttackAbility, new SpinAbilityState(
                _heroWeapon,
                _heroAttackComponent,
                _heroAbilityController,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData, true,
                canExit: (state) => state.Timer.Elapsed >= _heroAbilityController.ActiveAbility.ActiveTime));

            _stateMachine.AddState(nameof(HeroStompAbilityState), new HeroStompAbilityState(
                _heroWeapon,
                _heroAbilityController,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData));


            _stateMachine.AddState(AbilityCast, new AbilityCastState(
                _heroWeapon,
                _heroAbilityController,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                true, false,
                canExit: (state) => state.Timer.Elapsed >= _heroAbilityController.ActiveAbility.ActiveTime - 0.5f));

            _stateMachine.AddState(nameof(FirstAttackState), new FirstAttackState(
                _heroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                _vfxController,
                _heroAbilityController,
                needExitTime: true,
                isGhostState: false));
            
            _stateMachine.AddState(nameof(SecondAttackState), new SecondAttackState(
                _heroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                _vfxController,
                _heroAbilityController,
                needExitTime: true,
                isGhostState: false));
            
            _stateMachine.AddState(nameof(ThirdAttackState), new ThirdAttackState(
                _heroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                _vfxController,
                _heroAbilityController,
                needExitTime: true,
                isGhostState: false)); ;


            _stateMachine.AddTransitionFromAny(new Transition("", nameof(HeroDeathState),
                (transition) => _heroDeath.IsDead));

           _stateMachine.AddTransition(new TransitionAfter(nameof(FirstAttackState), HeroIdle,
               _heroWeapon
                   .WeaponData
                   .AttacksConfigs[0].AnimationData.Length));
           
           _stateMachine.AddTransition(new TransitionAfter(nameof(SecondAttackState), HeroIdle,
               _heroWeapon
                   .WeaponData
                   .AttacksConfigs[1].AnimationData.Length));
           _stateMachine.AddTransition(new TransitionAfter(nameof(ThirdAttackState), HeroIdle,
               _heroWeapon
                   .WeaponData
                   .AttacksConfigs[2].AnimationData.Length));

            _stateMachine.AddTwoWayTransition(new Transition(
                HeroIdle,
                HeroMovement,
                (transition) => _controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon,
                true));

            
            _stateMachine.AddTransition(new TransitionAfter(SpinAttackAbility, HeroIdle,
                _animationData.Animations[AnimationKey.Spinning].Length));


            _stateMachine.AddTransition(new TransitionAfter(
                nameof(HeroStompAbilityState),
                HeroIdle,
                _animationData.Animations[AnimationKey.Stomp].Length - 0.5f));

            _stateMachine.AddTransition(new TransitionAfter(
                AbilityCast,
                HeroIdle, _animationData.Animations[AnimationKey.SpellCast].Length - 0.4f));
            



            _stateMachine.InitStateMachine();
        }

        private void HandleAbilityUse(ActiveAbility ability)
        {
            if (ability.IsCastAbility)
            {
                _stateMachine.RequestStateChange(AbilityCast);
            }
            else if (ability.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id))
            {
                _stateMachine.RequestStateChange(SpinAttackAbility);
            }
            else if (ability.AbilityBlueprint.Identifier.Id.Equals(_stompAbilityId.Id))
            {
                _stateMachine.RequestStateChange(nameof(HeroStompAbilityState));
            }

            if (ability is AttackAbility attackAbility)
            {
                HandleAttackAbility(attackAbility);
            }
                
            
        }

        private void HandleAttackAbility(AttackAbility ability)
        {
            switch (ability.ComboCount)
            {
                case 1:
                    _stateMachine.RequestStateChange(nameof(FirstAttackState));
                    break;
                case 2:
                    _stateMachine.RequestStateChange(nameof(SecondAttackState));
                    break;
                case 3:
                    _stateMachine.RequestStateChange(nameof(ThirdAttackState));
                    break;
                default:
                    ability.ResetComboCounter();
                    _stateMachine.RequestStateChange(HeroIdle);
                    break;
            }
        }
    }
}