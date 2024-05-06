using Code.Runtime.ConfigData.Animations;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Entity.Hero.HeroStates;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Animator;
using Code.Runtime.Modules.StateMachine;
using Code.Runtime.Modules.StateMachine.Transitions;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroStateMachineHandler : MonoBehaviour
    {
        private IConfigProvider _configProvider;

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
        [SerializeField] private AbilityIdentifier _dodgeRollAbilityId;
        [SerializeField] private AbilityIdentifier _tornadoAbilityId;

        [SerializeField] private AnimationEventReceiver _eventReceiver;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroRotation _heroRotation;
        [SerializeField] private HeroAttackComponent _heroAttackComponent;
        [SerializeField] private HeroSkills _heroSkills;
        [SerializeField] private HeroWeapon _heroWeapon;
        [SerializeField] private TagController _tagController;
        [SerializeField] private AnimationDataContainer _animationData;
        [SerializeField] private HeroDeath _heroDeath;
        [SerializeField] private VisualEffectController _vfxController;


        [Inject]
        public void Construct(PlayerControls controls, AudioService audioService, IConfigProvider configProvider)
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
                _heroWeapon, _heroSkills, _heroAnimator, _heroMovement, _heroRotation, _animationData, true, false));

            _stateMachine.AddState(SpinAttackAbility, new SpinAbilityState(
                _heroWeapon,
                _heroAttackComponent,
                _heroSkills,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData, true, canExit: (state) => state.Timer.Elapsed >= _heroSkills.ActiveAbility.ActiveTime));

            _stateMachine.AddState(AbilityCast, new AbilityCastState(
                _heroWeapon,
                _heroSkills,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                true, false,
                canExit: (state) => state.Timer.Elapsed >= _heroSkills.ActiveAbility.ActiveTime));

            _stateMachine.AddState(HeroBaseAttack1, new FirstAttackState(
                _heroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                _vfxController,
                needExitTime: true,
                isGhostState: false,
                canExit: (state) =>
                    state.Timer.Elapsed >=
                    _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.FirstAttackStateDuration));

            _stateMachine.AddState(HeroBaseAttack2, new SecondAttackState(
                _heroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                _vfxController,
                needExitTime: true,
                isGhostState: false,
                canExit: (state) => state.Timer.Elapsed >=
                                    _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.SecondAttackStateDuration));

            _stateMachine.AddState(HeroBaseAttack3, new ThirdAttackState(
                _heroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                _animationData,
                needExitTime: true,
                isGhostState: false,
                canExit: (state) => state.Timer.Elapsed >=
                                    _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.ThirdAttackStateDuration));
            
            _stateMachine.AddTransitionFromAny(new Transition("", nameof(HeroDeathState),
                (transition) => _heroDeath.IsDead));

            _stateMachine.AddTransition(new TransitionAfter(
                HeroBaseAttack1,
                HeroIdle, _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.FirstAttackStateDuration + 0.1f));

            _stateMachine.AddTransition(new TransitionAfter(
                HeroBaseAttack2,
                HeroIdle, _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.SecondAttackStateDuration + 0.1f));

            _stateMachine.AddTransition(new TransitionAfter(
                HeroBaseAttack3,
                HeroIdle, _heroWeapon.GetEquippedWeaponData().WeaponFsmConfig.ThirdAttackStateDuration + 0.1f));

            _stateMachine.AddTwoWayTransition(new Transition(
                HeroIdle,
                HeroMovement,
                (transition) => _controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon,
                true));

            _stateMachine.AddTransition(new Transition(
                HeroMovement,
                HeroBaseAttack1,
                (transition) => _controls.Player.Attack.triggered));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                HeroBaseAttack1,
                (transition) => _controls.Player.Attack.WasPressedThisFrame()));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                HeroBaseAttack2,
                (transition) => _controls.Player.Attack.WasPressedThisFrame()));

            // _stateMachine.AddTransition(new Transition(
            //     HeroBaseAttack2,
            //     HeroBaseAttack3,
            //     (transition) => _controls.Player.Attack.WasPressedThisFrame()));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id)
            ));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack2,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id)
            ));

            _stateMachine.AddTransition(new Transition(
                AbilityCast,
                HeroIdle, (transition) => !_heroSkills.ActiveAbility.IsActive()));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack3,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id)
            ));
            
            _stateMachine.AddTransition(new Transition(
                HeroMovement,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id)
            ));

            _stateMachine.AddTransition(new Transition(
                SpinAttackAbility,
                HeroIdle,
                (transition) => _heroSkills.ActiveAbility != null &&
                                        !_heroSkills.ActiveAbility.IsActive() &&
                                        _heroSkills.ActiveAbility.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id)));

            _stateMachine.AddTransition(new Transition(
                SpinAttackAbility,
                HeroMovement,
                (transition) =>
                    _controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityBlueprint.Identifier.Id.Equals(_spinAttackAbilityId.Id)));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                AbilityCast,
                (transition) => _heroSkills.ActiveAbility != null &&
                                _heroSkills.ActiveAbility.IsActive() &&
                                _heroSkills.ActiveAbility.IsCastAbility, true));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack3,
                AbilityCast,
                (transition) => _heroSkills.ActiveAbility != null &&
                                _heroSkills.ActiveAbility.IsActive() &&
                                _heroSkills.ActiveAbility.IsCastAbility, true));


            _stateMachine.InitStateMachine();
        }
    }
}