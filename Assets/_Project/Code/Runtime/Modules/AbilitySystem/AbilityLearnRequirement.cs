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
            return false;
        }
    }
}