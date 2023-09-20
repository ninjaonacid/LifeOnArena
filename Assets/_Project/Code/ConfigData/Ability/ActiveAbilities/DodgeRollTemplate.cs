using UnityEngine;

namespace Code.ConfigData.Ability.ActiveAbilities
{
    [CreateAssetMenu(fileName = "DodgeRoll", menuName = "AbilityData/Dodge/DodgeRoll")]
    public class DodgeRollTemplate : AbilityTemplate<DodgeRoll>
    {
        public override IAbility GetAbility()
        {
            return new DodgeRoll(ActiveTime);
        }
    }
}
