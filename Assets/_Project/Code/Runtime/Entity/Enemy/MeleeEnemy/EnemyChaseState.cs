using System;
using Code.Runtime.ConfigData.Animations;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.MeleeEnemy
{
    public class EnemyChaseState : BaseEnemyState
    {
        private readonly NavMeshMoveToPlayer _moveToPlayer;

        public EnemyChaseState(NavMeshMoveToPlayer moveToPlayer, EnemyAnimator enemyAnimator,
            AnimationDataContainer animationData, bool needExitTime = false, bool isGhostState = false,
            Action<State<string, string>> onEnter = null, Action<State<string, string>> onLogic = null,
            Action<State<string, string>> onExit = null, Func<State<string, string>, bool> canExit = null) : base(
            enemyAnimator, animationData, needExitTime, isGhostState, onEnter, onLogic, onExit, canExit)
        {
            _moveToPlayer = moveToPlayer;
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayAnimation(_animationData.Animations[AnimationKey.Walking].Hash);
            _moveToPlayer.ShouldMove(true);
        }

        public override void OnLogic()
        {
            base.OnLogic();

            _moveToPlayer.SetDestination();
        }

        public override void OnExit()
        {
            base.OnExit();
            _moveToPlayer.ShouldMove(false);
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }
    }
}