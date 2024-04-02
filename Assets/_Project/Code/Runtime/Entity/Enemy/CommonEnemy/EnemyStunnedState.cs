using System;
using Code.Runtime.Modules.StateMachine.States;

namespace Code.Runtime.Entity.Enemy.CommonEnemy
{
    public class EnemyStunnedState : CommonEnemyState
    {
        private readonly TagController _tagController;

        public EnemyStunnedState(TagController tagController, EnemyAnimator enemyAnimator, bool needExitTime = false,
            bool isGhostState = false, Action<State<string, string>> onEnter = null,
            Action<State<string, string>> onLogic = null, Action<State<string, string>> onExit = null,
            Func<State<string, string>, bool> canExit = null) : base(enemyAnimator, needExitTime, isGhostState, onEnter,
            onLogic, onExit, canExit)
        {
            _tagController = tagController;
            
        }

        public override void OnEnter()
        {
            base.OnEnter();

            _canExit = (state) => !_tagController.HasTag("Stun");
        }

        
    

     
    }
}