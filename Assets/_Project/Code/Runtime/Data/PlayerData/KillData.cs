using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class KillData
    {
        public List<string> ClearedSpawners = new List<string>();
    }
}