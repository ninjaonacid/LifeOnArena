
using Code.Infrastructure.ObjectPool;

namespace Code.StaticData.Ability
{
    public enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
    public abstract class AbilityTemplateBase : AbilityBase
    {
        public string StateMachineId;
        public float Cooldown;
        public float CurrentCooldown;
        public float ActiveTime;
        public float CurrentActiveTime;
        public AbilityState State;

        protected IParticleObjectPool ParticlePool;

        public void InitServices(IParticleObjectPool particlePool)
        {
            ParticlePool = particlePool;
        }

        public abstract IAbility GetAbility();


        public virtual bool IsReady() =>
            State == AbilityState.Ready;
        
        public virtual bool IsActive() =>
            State == AbilityState.Active;

        private void OnEnable()
        {
            State = AbilityState.Ready;
        }
    }
}
