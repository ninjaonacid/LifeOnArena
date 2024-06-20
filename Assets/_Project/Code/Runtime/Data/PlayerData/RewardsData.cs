using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class RewardsData
    {
        public List<int> ClaimedRewards = new();
    }
}