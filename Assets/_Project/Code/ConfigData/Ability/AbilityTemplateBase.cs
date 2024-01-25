using Code.Core.Factory;
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
        public int Price;
        public AbilityState State;
        public ParticleObjectData ParticleObjectData;

        protected ParticleFactory _particleFactory;
        protected IBattleService _battleService;
        public abstract IAbility GetAbility();

        public void InitServices(
            ParticleFactory particleFactory,
            IBattleService battleService)
        {
            _particleFactory = particleFactory;
            _battleService = battleService;
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
