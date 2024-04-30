using System;
using UniRx;
using UnityEngine;

namespace Code.Runtime.Data.PlayerData
{
    [Serializable]
    public class PlayerExp
    {
        public event Action OnExpChanged;
        public event Action OnLevelChanged;
        public int Experience;
        public int Level;
        public int ExperienceToNextLevel;

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
                ExperienceToNextLevel = 100 * (Level * Level);
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
