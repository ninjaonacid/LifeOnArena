using UnityEngine;

namespace Code.ConfigData.Ability
{
    public interface IAbility
    {
        void Use(GameObject caster, GameObject target);
    }

}
