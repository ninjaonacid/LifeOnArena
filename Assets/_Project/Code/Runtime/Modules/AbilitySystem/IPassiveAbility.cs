using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public interface IPassiveAbility
    {
        void Apply(GameObject hero, PlayerData heroData);
    }
}
