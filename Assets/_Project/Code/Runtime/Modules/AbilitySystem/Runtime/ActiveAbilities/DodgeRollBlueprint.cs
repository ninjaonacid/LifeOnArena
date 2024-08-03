using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class DodgeRollBlueprint : ActiveAbilityBlueprint<DodgeDash>
    {
        public float DashSpeed;
        public override ActiveAbility GetAbility()
        {
            //return new DodgeDash(ActiveTime, DashSpeed);
            return null;
        }
    }
}
