using Code.Runtime.Entity;
using UnityEngine;


namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class Ability
    {
        public ActiveAbilityBlueprintBase AbilityBlueprint { get; }

        protected Ability(ActiveAbilityBlueprintBase abilityBlueprint)
        {
            AbilityBlueprint = abilityBlueprint;
        }

        public abstract void Use(GameObject caster, GameObject target);

        protected void ApplyEffects(GameObject target)
        {
            var statusController = target.GetComponentInParent<StatusEffectController>();

            foreach (var effect in AbilityBlueprint.GameplayEffects)
            {
                statusController.ApplyEffectToSelf(effect);
            }
        }

       
    }
}