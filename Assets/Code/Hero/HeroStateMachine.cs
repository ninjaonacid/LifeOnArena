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
        private IInputService _input;
        private HeroMovement _heroMovement;
        private HeroAnimator _heroAnimator;
        private HeroRotation _heroRotation;
        private SkillHolderData _skillHolderData;
        private Dictionary<Type, HeroBaseState> _states;
        private Dictionary<Type, List<HeroTransition>> _transitions;

        public bool IsInTransition { get; private set; }


        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _heroAnimator = GetComponent<HeroAnimator>();
            _heroMovement = GetComponent<HeroMovement>();
            _heroRotation = GetComponent<HeroRotation>();

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
                    new SpinAttackState(this, _input, _heroAnimator, _heroRotation),
            };

            _transitions = new Dictionary<Type, List<HeroTransition>>();

            InitState(_states[typeof(HeroIdleState)]);
        }

        public override void Update()
        {
            base.Update();
            
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _skillHolderData = progress.SkillHolderData;
            InitializeTransitions();
        }

        public async void DoTransition(HeroBaseState state)
        {
            var nextTransition = GetTransition(state);
            if (nextTransition == null) return;
            IsInTransition = true;

            if (state is HeroBaseAttackState attackState)
            {
                await UniTask.WaitUntil(() => attackState.IsEnded());
            }

            ChangeState(nextTransition.To);
            IsInTransition = false;
        }

        private HeroTransition GetTransition(HeroBaseState state)
        {
            foreach (var transition in _transitions[state.GetType()])
            {
                if (transition.Condition())
                {
                    return transition;
                }
            }

            return null;
        }

        private void AddTransition(HeroBaseState from, HeroBaseState to, Func<bool> condition)
        {
            if (from == null || to == null) return;
            
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<HeroTransition>();
                _transitions[from.GetType()] = transitions;
            }
        
            transitions.Add(new HeroTransition(to, condition));
        }

        private void InitializeTransitions()
        {
            AbilityId firstSkill = _skillHolderData.AbilityID[0];
            AbilityId secondSkill = _skillHolderData.AbilityID[1];
            AbilityId thirdSkill = _skillHolderData.AbilityID[2];

            Func<bool> AttackPressed() => () => _input.isAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => _input.isSkillButton1();
            Func<bool> SecondSkillPressed() => () => _input.isSkillButton2();
            Func<bool> ThirdSkillPressed() => () => _input.isSkillButton3();
            Func<bool> StateDurationEnd() => () => GetCurrentState().IsEnded();
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

            AddTransition(GetState<ThirdAttackState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetSkillState(secondSkill), SecondSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetSkillState(thirdSkill), ThirdSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());
            
            AddTransition(GetState<HeroIdleState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetSkillState(secondSkill), SecondSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetSkillState(thirdSkill), ThirdSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<HeroMovementState>(), MovementStart());

            AddTransition(GetState<HeroMovementState>(), GetState<HeroIdleState>(), MovementEnd());
            AddTransition(GetState<HeroMovementState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(firstSkill), FirstSkillPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(secondSkill), SecondSkillPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(thirdSkill), ThirdSkillPressed());

            AddTransition(GetSkillState(firstSkill), GetState<HeroIdleState>(), StateDurationEnd());
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

        private HeroBaseAttackState GetCurrentState() =>
            CurrentState as HeroBaseAttackState;

        private HeroBaseState GetState<TState>() =>
            _states[typeof(TState)];
    }
}
