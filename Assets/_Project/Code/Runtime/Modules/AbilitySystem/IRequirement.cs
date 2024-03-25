using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Modules.AbilitySystem
{
    public interface IRequirement
    {
        public bool CheckRequirement(PlayerData playerData);
    }
}