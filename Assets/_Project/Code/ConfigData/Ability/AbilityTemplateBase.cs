using Code.Core.ObjectPool;
using Code.Services.BattleService;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Code.ConfigData.Ability
{
    public enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }
    public abstract class AbilityTemplateBase : AbilityBase
    {
        public float Cooldown;
        public float CurrentCooldown;
        public float ActiveTime;
        public float CurrentActiveTime;
        public AbilityState State;
        public ParticleData ParticleData;

        protected ParticleObjectPool ParticlePool;
        protected IBattleService BattleService;
        public abstract IAbility GetAbility();

        public void InitServices(
            ParticleObjectPool particlePool, 
            IBattleService battleService)
        {
            ParticlePool = particlePool;
            BattleService = battleService;
        }

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
