using UnityEngine;

namespace Code.StaticData.Ability
{
    public interface IPassiveAbility
    {
        void Apply(GameObject hero);
    }
}
