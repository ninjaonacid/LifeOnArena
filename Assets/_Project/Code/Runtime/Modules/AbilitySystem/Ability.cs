using System.Collections.Generic;
using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Entity;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class Ability
    {
        public AbilityIdentifier AbilityIdentifier { get; private set; }
        protected IReadOnlyList<GameplayEffect> _effects;
        
        protected Ability(IReadOnlyList<GameplayEffect> effects, AbilityIdentifier identifier)
        {
            _effects = effects;
            AbilityIdentifier = identifier;
        }

        public abstract void Use(GameObject caster, GameObject target);

        public void ApplyEffects(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();

            foreach (var effect in _effects)
            {
                statusController.ApplyEffectToSelf(effect);
            }
        }
        
    }
}