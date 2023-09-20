using Code.Data;
using UnityEngine;

namespace Code.ConfigData.Ability
{
    public interface IPassiveAbility
    {
        void Apply(GameObject hero, PlayerData heroData);
    }
}
