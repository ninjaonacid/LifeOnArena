using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    [CreateAssetMenu(fileName = "DodgeRoll", menuName = "Config/AbilityData/Dodge/DodgeRoll")]
    public class DodgeRollBlueprint : ActiveAbilityBlueprint<DodgeDash>
    {
        public float DashSpeed;
        public override IAbility GetAbility()
        {
            return new DodgeDash(ActiveTime, DashSpeed);
        }
    }
}
