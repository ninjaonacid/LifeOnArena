using System;
using System.Collections.Generic;
using Code.Hero.HeroStates;
using Code.Logic;
using Code.Services.Input;
using Code.StateMachine;
using Code.StaticData.Ability;
using Code.UI.HUD.Skills;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Hero
{
    [RequireComponent(typeof(HeroAnimator), typeof(HeroMovement))]
    public class HeroStateMachine : BaseStateMachine
    {
        private IInputService _input;

        [SerializeField] private HeroMovement _heroMovement;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private HeroRotation _heroRotation;
        [SerializeField] private HeroAttack _heroAttack;
        [SerializeField] private HeroSkills _heroSkills;


        private Dictionary<Type, HeroBaseState> _states;
        private Dictionary<Type, List<HeroTransition>> _transitions;
        public bool IsInTransition { get; private set; }

        public void Construct(IInputService input)
        {
            _input = input;

            _heroSkills.OnSkillChanged += BuildTransitions;
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
                [typeof(RollState)] =
                    new RollState(this, _input, _heroAnimator, _heroAttack),
            };


            _transitions = new Dictionary<Type, List<HeroTransition>>();

            InitState(_states[typeof(HeroIdleState)]);
        }

        private void BuildTransitions()
        {
            _transitions.Clear();
            HeroAbilityData WeaponAbility = _heroSkills.GetSkillSlotAbility(SkillSlotID.WeaponSkillSlot);
            HeroAbilityData DodgeAbility = _heroSkills.GetSkillSlotAbility(SkillSlotID.Dodge);
            HeroAbilityData RageAbility = _heroSkills.GetSkillSlotAbility(SkillSlotID.Rage);

            Func<bool> AttackPressed() => () => _input.IsAttackButtonUp();
            Func<bool> FirstSkillPressed() => () => _input.IsSkillButton1() && WeaponAbility.IsAbilityReady();
            Func<bool> SecondSkillPressed() => () => _input.IsSkillButton2() && DodgeAbility.IsAbilityReady();
            Func<bool> ThirdSkillPressed() => () => _input.IsSkillButton3() && RageAbility.IsAbilityReady();
            Func<bool> StateDurationEnd() => () => GetCurrentState().IsEnded();
            Func<bool> MovementStart() => () => _input.Axis.sqrMagnitude > Constants.Epsilon;
            Func<bool> MovementEnd() => () => _input.Axis.sqrMagnitude < Constants.Epsilon;

            AddTransition(GetState<FirstAttackState>(), GetState<SecondAttackState>(), AttackPressed());
            AddTransition(GetState<FirstAttackState>(), GetSkillState(WeaponAbility), FirstSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetSkillState(DodgeAbility), SecondSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetSkillState(RageAbility), ThirdSkillPressed());
            AddTransition(GetState<FirstAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<SecondAttackState>(), GetState<ThirdAttackState>(), AttackPressed());
            AddTransition(GetState<SecondAttackState>(), GetSkillState(WeaponAbility), FirstSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetSkillState(DodgeAbility), SecondSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetSkillState(RageAbility), ThirdSkillPressed());
            AddTransition(GetState<SecondAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<ThirdAttackState>(), GetSkillState(WeaponAbility), FirstSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetSkillState(DodgeAbility), SecondSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetSkillState(RageAbility), ThirdSkillPressed());
            AddTransition(GetState<ThirdAttackState>(), GetState<HeroIdleState>(), StateDurationEnd());

            AddTransition(GetState<HeroIdleState>(), GetSkillState(WeaponAbility), FirstSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetSkillState(DodgeAbility), SecondSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetSkillState(RageAbility), ThirdSkillPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroIdleState>(), GetState<HeroMovementState>(), MovementStart());

            AddTransition(GetState<HeroMovementState>(), GetState<HeroIdleState>(), MovementEnd());
            AddTransition(GetState<HeroMovementState>(), GetState<FirstAttackState>(), AttackPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(WeaponAbility), FirstSkillPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(DodgeAbility), SecondSkillPressed());
            AddTransition(GetState<HeroMovementState>(), GetSkillState(RageAbility), ThirdSkillPressed());
            AddTransition(GetSkillState(WeaponAbility), GetState<HeroIdleState>(), StateDurationEnd());
            AddTransition(GetSkillState(DodgeAbility), GetState<HeroIdleState>(), StateDurationEnd());
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

        private HeroBaseState GetSkillState(HeroAbilityData ability)
        {
            switch (ability?.AbilityId)
            {
                case AbilityId.SpinAttack:
                    {
                        return GetState<SpinAttackState>();
                    }

                case AbilityId.FastSlash:
                    {
                        return null;
                    }
                case AbilityId.Dash:
                    {
                        return GetState<RollState>();
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
