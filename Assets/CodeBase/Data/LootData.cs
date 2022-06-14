using System;

namespace CodeBase.Data
{
    [Serializable]
    public class LootData
    {
        public int Collected;

        public Action CountChanged;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            CountChanged?.Invoke();
        }
    }
}