using UnityEngine;

namespace Code.Enemy.CommonEnemy
{
    public class EnemyIdleState : CommonEnemyState
    {
        private readonly EnemyTarget _target;
        private readonly Aggression _aggression;
        
        public EnemyIdleState(EnemyAnimator enemyAnimator, 
            EnemyTarget enemyTarget, 
            Aggression agression,
            bool needsExitTime, bool isGhostState = false) : base(enemyAnimator, needsExitTime, isGhostState)
        {
            _target = enemyTarget;
            _aggression = agression;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _enemyAnimator.PlayIdle();

        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (_aggression.HasTarget)
            {
                _target.RotationToTarget();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }

        public override bool IsStateOver()
        {
            return base.IsStateOver();
        }
    }
}
