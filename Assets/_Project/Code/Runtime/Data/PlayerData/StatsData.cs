using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class StatsData
    {
        public readonly Dictionary<string, int> StatsValues = new Dictionary<string, int>();
        public readonly Dictionary<string, int> StatsCapacities = new Dictionary<string, int>();
        public readonly Dictionary<string, int> StatPerLevel = new Dictionary<string, int>();
        public int StatUpgradePrice;
    }
}
