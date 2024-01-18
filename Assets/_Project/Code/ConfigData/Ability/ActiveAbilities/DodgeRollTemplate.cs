using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "DodgeRoll", menuName = "Config/AbilityData/Dodge/DodgeRoll")]
    public class DodgeRollTemplate : AbilityTemplate<DodgeDash>
    {
        public float DashSpeed;
        public override IAbility GetAbility()
        {
            return new DodgeDash(ActiveTime, DashSpeed);
        }
    }
}
