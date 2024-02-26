using UnityEngine;

namespace Code.Runtime.ConfigData.Ability
{
    public interface IAbility
    {
        void Use(GameObject caster, GameObject target);
    }

}
