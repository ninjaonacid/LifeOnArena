using UnityEngine;

namespace Code.StaticData.Ability
{
    public interface IAbility
    {
        void Use(GameObject caster, GameObject target);
    }
}
