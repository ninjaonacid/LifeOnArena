using System.Collections.Generic;
using Code.Runtime.Entity;
using Code.Runtime.Entity.StatusEffects;
using UnityEngine;


namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class Ability
    {
        public ActiveAbilityBlueprintBase AbilityBlueprint { get; }

        protected readonly List<GameplayEffect> _gameplayEffects = new();
        
        public AbilityController Owner { get; private set; }

        protected Ability(ActiveAbilityBlueprintBase abilityBlueprint)
        {
            AbilityBlueprint = abilityBlueprint;
        }

        public void Initialize(AbilityController owner)
        {
            Owner = owner;
        }

        public abstract void Use(AbilityController caster, GameObject target);

        protected void ApplyEffects(GameObject target)
        {
            var statusController = target.GetComponentInParent<StatusEffectController>();

            foreach (var effect in AbilityBlueprint.GameplayEffectsBlueprints)
            {
                statusController.ApplyEffectToSelf(effect.GetGameplayEffect(Owner));
            }
        }
    }
}