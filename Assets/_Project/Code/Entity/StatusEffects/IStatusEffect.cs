using UnityEngine;

namespace Code.Entity.StatusEffects
{
    public interface IStatusEffect
    {
        public void Apply(GameObject target);
    }
}
