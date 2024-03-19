using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "SpinAttackAbility", menuName = "AbilityData/Attack/SpinAttack")]
    public class SpinAttackBlueprint : ActiveAbilityBlueprint<SpinAttack>
    {
        public override ActiveAbility GetAbility()
        {
            return new SpinAttack(this);
            
        }
    }

    public class SpinAttack : ActiveAbility
    {
        
        public override void Use(GameObject caster, GameObject target)
        {
        }

        public SpinAttack(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
        }
    }
}