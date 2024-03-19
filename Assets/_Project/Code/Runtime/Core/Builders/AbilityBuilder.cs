using Code.Runtime.Modules.AbilitySystem;
using Sirenix.Serialization;
using UnityEngine;

namespace Code.Runtime.Core.Builders
{
    public class AbilityBuilder<TAbility> where TAbility : ActiveAbility
    {
        public TAbility _ability;

        private float _cooldown;

        protected readonly AbilityBuilder<TAbility> _builder;
        
        public AbilityBuilder()
        {
            _builder = this;
        }

        public AbilityBuilder<TAbility> SetCooldown(float cooldown)
        {
            _cooldown = cooldown;
            return this;
        }

        public TAbility Build()
        {
            return null;
        }
    }
}
