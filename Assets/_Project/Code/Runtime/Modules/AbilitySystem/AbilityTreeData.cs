using System;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    [Serializable]
    public struct AbilityTreeData
    {
        public AbilityTreeBranch Branch;
        public int Position;
        public int Price;
        public AbilityLearnRequirement Requirement => new(Position);
    }
}