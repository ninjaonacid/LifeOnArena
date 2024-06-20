using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class StatsData
    {
        public readonly Dictionary<string, int> Stats = new Dictionary<string, int>();
    }
}
