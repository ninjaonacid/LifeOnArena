using System;
using Newtonsoft.Json;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class LootData
    {
        public int Collected;

        [JsonIgnore]
        public Action<int> CountChanged;

        
        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            CountChanged?.Invoke(loot.Value);
        }

        public void Collect(int value)
        {
            Collected += value;
            CountChanged?.Invoke(value);
        }
    }
}