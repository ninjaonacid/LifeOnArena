using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Enemy.CommonEnemy
{
    public class EnemyPatrolState : CommonEnemyState
    {
        public EnemyPatrolState(EnemyAnimator enemyAnimator, bool needsExitTime, bool isGhostState = false) : 
        base(enemyAnimator, needsExitTime, isGhostState)
        {
            
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic();

        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnExitRequest()
        {
            base.OnExitRequest();
        }

    }
}
