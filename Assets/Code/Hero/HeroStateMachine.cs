using System;
using System.Collections.Generic;
using Code.Data;
using Code.Hero.HeroStates;
using Code.Logic;
using Code.Services;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.StateMachine;
using Code.StaticData.Ability;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroMovement))]
    public class HeroStateMachine : BaseStateMachine, ISavedProgressReader
    {
        private HeroMovement _heroMovement;
        private HeroAnimator _heroAnimator;
        private HeroSkills _heroSkills;
        private Dictionary<Type, HeroBaseState> _states;
        private HeroTransition _currentTransition;
        private Dictionary<Type, List<HeroTransition>> _transitions;
        private IInputService _input;

        private SkillsData _skillsData;


        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroMovement = GetComponent<HeroMovement>();
            _heroSkills = GetComponent<HeroSkills>();

            _states = new Dictionary<Type, HeroBaseState>
            {
                [typeof(FirstAttackState)] =
                    new FirstAttackState(this, _input, _heroAnimator),
                [typeof(SecondAttackState)] =
                    new SecondAttackState(this, _input, _heroAnimator),
                [typeof(ThirdAttackState)] =
                    new ThirdAttackState(this, _input, _heroAnimator),
                [typeof(HeroIdleState)] =
                    new HeroIdleState(this, _input, _heroAnimator),
                [typeof(HeroMovementState)] =
                    new HeroMovementState(this, _input, _heroAnimator, _heroMovement),
                [typeof(SpinAttackState)] =
                    new SpinAttackState(this, _input, _heroAnimator),
            };

            _transitions = new Dictionary<Type, List<HeroTransition>>();

            InitState(_states[typeof(HeroIdleState)]);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _skillsData = progress.SkillsData;
            InitializeTransitions();
        }

        private void AddTransition(HeroBaseState from, HeroBaseState to, Func<bool> condition)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<HeroTransition>();
                _transitions[from.GetType()] = transitions;
            }
            transitions.Add(new HeroTransition(to, condition));
        }

        private void GetTransition(HeroBaseState state)
        {
            foreach (var transition in _transitions[state.GetType()])
            {
                if (transition.Condition())
                {
                    _currentTransition = transition;
                    break;
                }
            }
        }

        private void InitializeTransitions()
        {
            AbilityId firstSkill = _skillsData.AbilityID[0];
            AbilityId secondSkill = _skillsData.AbilityID[1];
            AbilityId thirdSkill = _skillsData.AbilityID[2];

            Func<bool> AttackPressed() => () => _input.isAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => _input.isSkillButton1();
            Func<bool> SecondSkillPressed() => () => _input.isSkillButton2();
            Func<bool> ThirdSkillPressed() => () => _input.isSkillButton3();
            Func<bool> StateDurationEnd() => () => GetCurrentState().Duration <= 0;
            Func<bool> MovementStart() => () => _input.Axis.sqrMagnitude > Constants.Epsilon;
            Func<bool> MovementEnd() => () => _heroMovement.GetVelocity() <= 0;

            AddTransition(GetState<FirstAttackState>(), GetState<SecondAttackState>(), AttackPressed());
            AddTransition(GetState<FirstAttackState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetSkillState(secondSkill), SecondSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetSkillState(thirdSkill), ThirdSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<SecondAttackState>(), GetState<ThirdAttackState>(), AttackPressed());
            AddTransition(GetState<SecondAttackState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetSkillState(secondSkill), SecondSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetSkillState(thirdSkill), ThirdSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<HeroIdleState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<HeroMovementState>(), MovementStart());

            AddTransition(GetState<HeroMovementState>(), GetState<HeroIdleState>(), MovementEnd());
            AddTransition(GetState<HeroMovementState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(secondSkill), SecondSkillPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(thirdSkill), ThirdSkillPressed());
        }

        private HeroBaseState GetSkillState(AbilityId abilityId)
        {
            switch (abilityId)
            {
                case AbilityId.SpinAttack:
                {
                    return GetState<SpinAttackState>();
                }

                case AbilityId.FastSlash:
                {
                    return null;
                }
                default: return null;
            }
        }

        public void Enter<TState>() where TState : HeroBaseState => 
            ChangeState(GetState<TState>());

        public async void DoTransition(HeroBaseState state)
        {
            GetTransition(state);
            if (state is HeroBaseAttackState attackState)
            {
                await UniTask.WaitUntil(() => attackState.IsEnded);
            }
            ChangeState(_currentTransition.To);
        }

        private HeroBaseAttackState GetCurrentState() =>
            CurrentState as HeroBaseAttackState;

        private HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];
    }
}
