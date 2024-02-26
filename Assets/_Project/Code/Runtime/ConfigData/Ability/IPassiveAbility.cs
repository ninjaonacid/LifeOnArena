using Code.Runtime.Data.PlayerData;
using UnityEngine;

namespace Code.Runtime.ConfigData.Ability
{
    public interface IPassiveAbility
    {
        void Apply(GameObject hero, PlayerData heroData);
    }
}
