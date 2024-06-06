using Code.Runtime.Modules.Requirements;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public abstract class AbilityRequirementData<T> : ScriptableObject 
    {
        public abstract T CreateRequirement();
    }
}