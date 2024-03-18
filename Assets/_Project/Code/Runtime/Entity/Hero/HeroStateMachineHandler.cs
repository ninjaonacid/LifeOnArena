using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.Audio;
using Code.Runtime.Core.ConfigProvider;
using Code.Runtime.Entity.Hero.HeroStates;
using Code.Runtime.Logic;
using Code.Runtime.Modules.StateMachine;
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

        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroRotation _heroRotation;
        [SerializeField] private HeroAttackComponent HeroAttackComponent;
        [SerializeField] private HeroSkills _heroSkills;
        [SerializeField] private HeroWeapon _heroWeapon;
        [SerializeField] private TagController _tagController;


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
                _heroAnimator, _heroMovement, _heroRotation, false, true));

            _stateMachine.AddState(HeroMovement, new HeroMovementState(
                _heroAnimator, _heroMovement, _heroRotation, false, false));

            _stateMachine.AddState(RollAbility, new RollDodgeState(
                _heroWeapon, _heroSkills, _heroAnimator, _heroMovement, _heroRotation, true, false));

            _stateMachine.AddState(SpinAttackAbility, new SpinAbilityState(
                _heroWeapon, _heroSkills, _heroAnimator, _heroMovement, _heroRotation, true, false));

            _stateMachine.AddState(AbilityCast, new AbilityCastState(
                _heroWeapon, _heroSkills, _heroAnimator, _heroMovement, _heroRotation, true, true));

            _stateMachine.AddState(HeroBaseAttack1, new FirstAttackState(
                HeroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                needsExitTime: true,
                isGhostState: false));

            _stateMachine.AddState(HeroBaseAttack2, new SecondAttackState(
                HeroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                needsExitTime: true,
                isGhostState: false));

            _stateMachine.AddState(HeroBaseAttack3, new ThirdAttackState(
                HeroAttackComponent,
                _heroWeapon,
                _heroAnimator,
                _heroMovement,
                _heroRotation,
                needsExitTime: true,
                isGhostState: false));


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
                (transition) => _controls.Player.Attack.triggered));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                HeroBaseAttack2,
                (transition) => _controls.Player.Attack.triggered,
                false));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack2,
                HeroBaseAttack3,
                (transition) => _controls.Player.Attack.triggered,
                false));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack2,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack3,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityIdentifier.Id.Equals(_spinAttackAbilityId.Id)
            ));


            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack2,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityIdentifier.Id.Equals(_spinAttackAbilityId.Id)
            ));

            _stateMachine.AddTransitionFromAny(new Transition(
                "",
                HeroIdle,
                t => _stateMachine.ActiveState.IsStateOver() &&
                     _stateMachine.ActiveState is not HeroMovementState));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack3,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityIdentifier.Id.Equals(_spinAttackAbilityId.Id)
            ));

            _stateMachine.AddTransition(new Transition(
                SpinAttackAbility,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));

            _stateMachine.AddTransition(new Transition(
                SpinAttackAbility,
                HeroMovement,
                (transition) => _stateMachine.ActiveState.IsStateOver() &&
                                _controls.Player.Movement.ReadValue<Vector2>().sqrMagnitude > Constants.Epsilon));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityIdentifier.Id.Equals(_spinAttackAbilityId.Id)));

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

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                RollAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityIdentifier.Id.Equals(_dodgeRollAbilityId.Id)));

            _stateMachine.AddTransition(new Transition(
                RollAbility,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));

            _stateMachine.AddTransition(new Transition(
                HeroMovement,
                RollAbility,
                (transition) =>
                    _heroSkills.ActiveAbility != null &&
                    _heroSkills.ActiveAbility.IsActive() &&
                    _heroSkills.ActiveAbility.AbilityIdentifier.Id.Equals(_dodgeRollAbilityId.Id)));

            _stateMachine.InitStateMachine();
        }
    }
}