using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public interface IAbility
    {
        void Use(GameObject caster, GameObject target);
    }

}
