using UnityEngine;

namespace Code.StateMachine.Base
{
    public class StateBase<TState>
    {
        public bool NeedExitTime;
        public bool IsGhostState;
        public TState name;

        public IStateMachine<TState> fsm;

        public StateBase(bool needExitTime, bool isGhostState)
        {
            NeedExitTime = needExitTime;
            IsGhostState = isGhostState;
        }

        public virtual void Init()
        {
            
        }

        public virtual void OnEnter()
        {
            Debug.Log("active state" + name.ToString());
        }

        public virtual void OnLogic()
        {

        }

        public virtual void OnExit()
        {
            
        }

        public virtual void OnExitRequest()
        {
            
        }

        public virtual bool IsStateOver()
        {
            return true;
        }
    }

    public class StateBase : StateBase<string>
    {
        public StateBase(bool needsExitTime, bool isGhostState = false)
            : base(needsExitTime, isGhostState)
        {
        }
    }
}
