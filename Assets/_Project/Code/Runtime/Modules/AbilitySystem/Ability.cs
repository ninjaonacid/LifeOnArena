using Code.Runtime.Entity;
using UnityEngine;


namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class Ability
    {
        protected readonly ActiveAbilityBlueprintBase _abilityBlueprint;

        protected Ability(ActiveAbilityBlueprintBase abilityBlueprint)
        {
            _abilityBlueprint = abilityBlueprint;
        }

        public abstract void Use(GameObject caster, GameObject target);

        public void ApplyEffects(GameObject target)
        {
            var statusController = target.GetComponent<StatusEffectController>();

            foreach (var effect in _abilityBlueprint.StatusEffects)
            {
                statusController.ApplyEffectToSelf(effect);
            }
        }
        
    }
}