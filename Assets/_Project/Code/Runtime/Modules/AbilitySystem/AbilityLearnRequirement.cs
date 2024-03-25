using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityLearnRequirement : IRequirement
    {
        private int _abilityIndex;
        
        public AbilityLearnRequirement(int index)
        {
            _abilityIndex = index;
        }
        
        public bool CheckRequirement(PlayerData playerData)
        {
            var previousAbilityIndex = _abilityIndex - 1;
            
            if (previousAbilityIndex >= 0)
            {
                var ability = playerData.AbilityData.UnlockedAbilities[previousAbilityIndex];
                
                if (ability is not null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}