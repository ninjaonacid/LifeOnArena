using System;
using Code.Runtime.Modules.Requirements;
using Sirenix.Serialization;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [Serializable]
    public class AbilityTreeData
    {
        public AbilityTreeBranch Branch;
        public int Position;
        public int Price;
        [SerializeReference] public IRequirement Requirement;
    }
}