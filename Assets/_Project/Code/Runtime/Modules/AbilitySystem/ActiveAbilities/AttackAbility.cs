using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class AttackAbility : ActiveAbility
    {
        
        public override void Use(GameObject caster, GameObject target)
        {
        }

        public AttackAbility(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
        }
    }
}