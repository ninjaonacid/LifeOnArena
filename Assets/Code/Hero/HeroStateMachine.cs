using System;
using System.Collections.Generic;
using Code.Hero.HeroStates;
using Code.Logic;
using Code.Services.Input;
using Code.Services.PersistentProgress;
using Code.StateMachine;
using Code.StaticData.Ability;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroMovement))]
    public class HeroStateMachine : BaseStateMachine
    {
        private IInputService _input;
        private IProgressService _progress;
        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroRotation _heroRotation;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroSkills _heroSkills;
   
        private Dictionary<Type, HeroBaseState> _states;
        private Dictionary<Type, List<HeroTransition>> _transitions;

        public bool IsInTransition { get; private set; }

        public void Construct(IInputService input, IProgressService progress)
        {
            _input = input;
            _progress = progress;
            _progress.Progress.skillHudData.WeaponSkillChanged += ChangeStateMachine;

            InitializeStateMachine();
            BuildTransitions();
        }

        public override void Update()
        {
            base.Update();
            var nextTransition = GetTransition(CurrentState);
            if (nextTransition != null && !IsInTransition)
            {
                DoTransition(nextTransition);
            }
        }

        private void InitializeStateMachine()
        {
            _states = new Dictionary<Type, HeroBaseState>
            {
                [typeof(FirstAttackState)] =
                    new FirstAttackState(this, _input, _heroAnimator, _heroAttack),
                [typeof(SecondAttackState)] =
                    new SecondAttackState(this, _input, _heroAnimator, _heroAttack),
                [typeof(ThirdAttackState)] =
                    new ThirdAttackState(this, _input, _heroAnimator, _heroAttack),
                [typeof(HeroIdleState)] =
                    new HeroIdleState(this, _input, _heroAnimator),
                [typeof(HeroMovementState)] =
                    new HeroMovementState(this, _input, _heroAnimator, _heroMovement),
                [typeof(SpinAttackState)] =
                    new SpinAttackState(this, _input, _heroAnimator, _heroRotation, _heroAttack),
            };

            _transitions = new Dictionary<Type, List<HeroTransition>>();

            InitState(_states[typeof(HeroIdleState)]);
        }

        private void ChangeStateMachine(HeroAbility obj)
        {

            // FindTransitions(GetSkillState(weaponSkill));

        }

        private void BuildTransitions()
        {
            Func<bool> AttackPressed() => () => _input.isAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => _input.isSkillButton1();
            Func<bool> SecondSkillPressed() => () => _input.isSkillButton2();
            Func<bool> ThirdSkillPressed() => () => _input.isSkillButton3();
            Func<bool> StateDurationEnd() => () => GetCurrentState().IsEnded();
            Func<bool> MovementStart() => () => _input.Axis.sqrMagnitude > Constants.Epsilon;
            Func<bool> MovementEnd() => () => _input.Axis.sqrMagnitude < Constants.Epsilon;

            AddTransition(GetState<FirstAttackState>(), GetState<SecondAttackState>(), AttackPressed());
            //AddTransition(GetState<FirstAttackState>(), GetSkillState(weaponSkill), FirstSkillPressed());
            //AddTransition(GetState<FirstAttackState>(), GetSkillState(dodgeSkill), SecondSkillPressed());
            //AddTransition(GetState<FirstAttackState>(), GetSkillState(rageSkill), ThirdSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<SecondAttackState>(), GetState<ThirdAttackState>(), AttackPressed());
            //AddTransition(GetState<SecondAttackState>(), GetSkillState(weaponSkill), FirstSkillPressed());
            //AddTransition(GetState<SecondAttackState>(), GetSkillState(dodgeSkill), SecondSkillPressed());
            //AddTransition(GetState<SecondAttackState>(), GetSkillState(rageSkill), ThirdSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            //AddTransition(GetState<ThirdAttackState>(), GetSkillState(weaponSkill), FirstSkillPressed());
            //AddTransition(GetState<ThirdAttackState>(), GetSkillState(dodgeSkill), SecondSkillPressed());
            //AddTransition(GetState<ThirdAttackState>(), GetSkillState(rageSkill), ThirdSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            //AddTransition(GetState<HeroIdleState>(), GetSkillState(weaponSkill), FirstSkillPressed());
            //AddTransition(GetState<HeroIdleState>(), GetSkillState(dodgeSkill), SecondSkillPressed());
            //AddTransition(GetState<HeroIdleState>(), GetSkillState(rageSkill), ThirdSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<HeroMovementState>(), MovementStart());

            AddTransition(GetState<HeroMovementState>(), GetState<HeroIdleState>(), MovementEnd());
            AddTransition(GetState<HeroMovementState>(), GetState<FirstAttackState>(), AttackPressed());
            //AddTransition(GetState<HeroMovementState>(), GetSkillState(weaponSkill), FirstSkillPressed());
            //AddTransition(GetState<HeroMovementState>(), GetSkillState(dodgeSkill), SecondSkillPressed());
            //AddTransition(GetState<HeroMovementState>(), GetSkillState(rageSkill), ThirdSkillPressed());

            //AddTransition(GetSkillState(weaponSkill), GetState<HeroIdleState>(), StateDurationEnd());
        }

        private void FindTransitions(HeroBaseState stateToRemove)
        {
            foreach (var item in _transitions)
            {
                var result = item.Value;
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].To.GetType() == stateToRemove.GetType())
                    {
                        result.RemoveAt(i);
                    }
                }
            }
        }
        public async void DoTransition(HeroTransition transition)
        {
            IsInTransition = true;

            if (CurrentState is HeroBaseAttackState attackState)
            {
                await UniTask.WaitUntil(() => attackState.IsEnded(), cancellationToken: this.GetCancellationTokenOnDestroy());
            }

            ChangeState(transition.To);

            IsInTransition = false;
        }

        private HeroTransition GetTransition(State state)
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
