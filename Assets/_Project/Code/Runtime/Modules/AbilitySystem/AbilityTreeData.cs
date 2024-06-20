using System;
using System.Collections.Generic;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.Requirements;
using Newtonsoft.Json;
using Sirenix.OdinInspector;

namespace Code.Runtime.Modules.AbilitySystem
{
    [Serializable]
    public class AbilityTreeData
    {
        public AbilityTreeBranch Branch;
        public int Position;
        public int Price;
        [FoldoutGroup("AbilityUnlockRequirements", true)]
        public List<IRequirement<PlayerData>> UnlockRequirements;
    }
}