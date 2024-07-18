using System;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class PlayerExp
    {
        public event Action OnExpChanged;
        public event Action OnLevelChanged;
        [JsonProperty] public float Experience { get; private set; }
        [JsonProperty] public int Level { get; private set; }
        public float ExperienceToNextLevel;
        public float ExponentialFactor;

        public PlayerExp()
        {
            Level = 1;
            Experience = 0;
            ExperienceToNextLevel = 100;
        }
        
        public void AddExperience(int amount)
        {
            Experience += amount;
            if (Experience >= ExperienceToNextLevel)
            {
                Level++;
                Experience -= ExperienceToNextLevel;
                ExperienceToNextLevel = 100 * (Level * ExponentialFactor);
                OnLevelChanged?.Invoke();
            }
            
            Debug.Log($"Experience added  {Experience}");
            
            OnExpChanged?.Invoke();
        }

        public IObservable<Unit> OnExperienceChangedAsObservable()
        {
            return Observable.FromEvent(
                addHandler => OnExpChanged += addHandler,
                removeHandler => OnExpChanged -= removeHandler);
        }

        public IObservable<Unit> OnLevelChangedAsObservable()
        {
            return Observable.FromEvent(
                addHandler => OnLevelChanged += addHandler,
                removeHandler => OnLevelChanged += removeHandler);
        }
    }
}
