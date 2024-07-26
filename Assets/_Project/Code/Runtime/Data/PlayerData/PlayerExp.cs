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

        private const float BaseExperience = 100f;
        [JsonProperty] public int Level { get; private set; }
        [JsonProperty] public float CurrentExperience { get; private set; }

        public float ProgressToNextLevel => CurrentExperience / ExperienceToNextLevel;
        public float ExperienceToNextLevel => BaseExperience * Mathf.Pow(Level, ExponentialFactor);
        public float ExponentialFactor;

        public PlayerExp()
        {
            Level = 1;
            CurrentExperience = 0;
        }
        
        public void AddExperience(float amount)
        {
            CurrentExperience += amount;
            
            if (CurrentExperience >= ExperienceToNextLevel)
            {
                CurrentExperience -= ExperienceToNextLevel;
                Level++;
                OnLevelChanged?.Invoke();
            }

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
