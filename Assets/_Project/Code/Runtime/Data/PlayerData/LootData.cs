using System;
using Newtonsoft.Json;
using UniRx;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class LootData
    {
        public int Collected;

        public ReactiveProperty<int> CollectedLoot = new();
        
        [JsonIgnore]
        public Action<int> CountChanged;

        
        public void Collect(SoulLoot soulLoot)
        {
            Collected += soulLoot.Value;
            CollectedLoot.Value += soulLoot.Value;
            CountChanged?.Invoke(soulLoot.Value);
        }

        public void Collect(int value)
        {
            Collected += value;
            CollectedLoot.Value += value;
            CountChanged?.Invoke(value);
        }

        public void Spend(int value)
        {
            Collected -= value;
            CollectedLoot.Value -= value;
            CountChanged?.Invoke(value);
        }
        
        public IObservable<int> OnLootChangedAsObservable()
        {
            return Observable.FromEvent<int>(
                addHandler => CountChanged += addHandler,
                removeHandler => CountChanged -= removeHandler);
        }
    }
}