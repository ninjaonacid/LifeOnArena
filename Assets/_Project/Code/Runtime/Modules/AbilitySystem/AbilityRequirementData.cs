using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class AbilityRequirementData<T> : ScriptableObject where T : IRequirement
    {
        public abstract T CreateRequirement();
    }
}