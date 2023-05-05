using Code.Hero.HeroStates;
using Code.Logic;
using Code.Logic.StateMachine;
using Code.Services.AudioService;
using Code.Services.Input;
using Code.StaticData;
using UnityEngine;
using VContainer;

namespace Code.Hero
{
    public class HeroStateMachineHandler : MonoBehaviour
    {
        private const string HeroIdle = "HeroIdle";
        private const string HeroMovement = "HeroMovement";
        private const string AbilityCast = "AbilityCast";
        private const string SpinAttackAbility = "SpinAttackAbility";
        private const string RollAbility = "RollAbility";
        private const string HeroBaseAttack1 = "HeroBaseAttack1";
        private const string HeroBaseAttack2 = "HeroBaseAttack2";
        private const string HeroBaseAttack3 = "HeroBaseAttack3";

        private FiniteStateMachine _stateMachine;
        private IInputService _input;
        private IAudioService _audioService;

        [SerializeField] private Identifier _spinAttackAbilityID;
        [SerializeField] private Identifier _dodgeRollAbilityID;
        [SerializeField] private Identifier _tornadoAbilityID;
     
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroRotation _heroRotation;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroSkills _heroSkills;


        [Inject]
        public void Construct(IInputService inputService, IAudioService audioService)
        {
            _input = inputService;
            _audioService = audioService;
        }

        void Update()
        {
            _stateMachine.OnLogic();
        }

        void Start()
        {
            _stateMachine = new FiniteStateMachine();

            _stateMachine.AddState(HeroIdle, new HeroIdleState(
                _heroAnimator, false, true));

            _stateMachine.AddState(HeroMovement, new HeroMovementState(
                _heroAnimator, _heroMovement, false, false));

            _stateMachine.AddState(RollAbility, new RollDodgeState(
                _heroAnimator, _heroAttack, _heroMovement, _heroRotation, true, false));

            _stateMachine.AddState(SpinAttackAbility, new SpinAbilityState(
                _heroAnimator, _heroAttack, _heroRotation, true, false));

            _stateMachine.AddState(HeroBaseAttack1, new FirstAttackState(
                _heroAnimator,
                _heroAttack,
                needExitTime: true,
                isGhostState: false));

            _stateMachine.AddState(HeroBaseAttack2, new SecondAttackState(
                _heroAnimator,
                _heroAttack,
                needExitTime: true,
                isGhostState: false));

            _stateMachine.AddState(HeroBaseAttack3, new ThirdAttackState(
                _heroAnimator,
                _heroAttack,
                needExitTime: true,
                isGhostState: false));

            _stateMachine.AddTwoWayTransition(new Transition(
                HeroIdle,
                HeroMovement,
                (transition) => _input.Axis.sqrMagnitude > Constants.Epsilon,
                true));

            _stateMachine.AddTransition(new Transition(
                HeroMovement,
                HeroBaseAttack1,
                (transition) => _input.IsAttackButtonUp()));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                HeroBaseAttack1,
                (transition) => _input.IsAttackButtonUp()));

            
            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                HeroBaseAttack2,
                (transition) => _input.IsAttackButtonUp(),
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
                HeroBaseAttack2,
                HeroBaseAttack3,
                (transition) => _input.IsAttackButtonUp(),
                false));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack1,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveSkill != null &&
                    _heroSkills.ActiveSkill.IsActive() &&
                    _heroSkills.ActiveSkill.Identifier.Id.Equals(_spinAttackAbilityID.Id)
            ));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack2,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveSkill != null &&
                    _heroSkills.ActiveSkill.IsActive() &&
                    _heroSkills.ActiveSkill.Identifier.Id.Equals(_spinAttackAbilityID.Id)
            ));

            _stateMachine.AddTransition(new Transition(
                HeroBaseAttack3,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveSkill != null &&
                    _heroSkills.ActiveSkill.IsActive() &&
                    _heroSkills.ActiveSkill.Identifier.Id.Equals(_spinAttackAbilityID.Id)
            ));

            _stateMachine.AddTransition(new Transition(
                SpinAttackAbility,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));

            _stateMachine.AddTransition(new Transition(
                SpinAttackAbility,
                HeroMovement,
                (transition) => _stateMachine.ActiveState.IsStateOver() &&
                                _input.Axis.sqrMagnitude > Constants.Epsilon));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                SpinAttackAbility,
                (transition) =>
                    _heroSkills.ActiveSkill != null &&
                    _heroSkills.ActiveSkill.IsActive() &&
                    _heroSkills.ActiveSkill.Identifier.Id.Equals(_spinAttackAbilityID.Id)));

            _stateMachine.AddTransition(new Transition(
                HeroIdle,
                RollAbility,
                (transition) =>
                    _heroSkills.ActiveSkill != null &&
                    _heroSkills.ActiveSkill.IsActive() &&
                    _heroSkills.ActiveSkill.Identifier.Id.Equals(_dodgeRollAbilityID.Id)
                    ));

            _stateMachine.AddTransition(new Transition(
                RollAbility,
                HeroIdle,
                (transition) => _stateMachine.ActiveState.IsStateOver()));

            _stateMachine.AddTransition(new Transition(
                HeroMovement,
                RollAbility,
                (transition) =>
                        _heroSkills.ActiveSkill != null &&
                        _heroSkills.ActiveSkill.IsActive() &&
                        _heroSkills.ActiveSkill.Identifier.Id.Equals(_dodgeRollAbilityID.Id)));

            _stateMachine.InitStateMachine();
        }
    }
}
