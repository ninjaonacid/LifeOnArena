using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class FireWallBlueprint : ActiveAbilityBlueprint<FireWall>
    {
        public override ActiveAbility GetAbility()
        {
            return new FireWall(this);
        }
    }

    public class FireWall : ActiveAbility
    {
        public FireWall(ActiveAbilityBlueprintBase abilityBlueprint) : base(abilityBlueprint)
        {
        }
        

        public override void Use(GameObject caster, GameObject target)
        {
           
        }
    }
}
