using System;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class HeroExp
    {
        public event Action OnExpChanged;
        public int Experience;
        public int Level;
        public int ExperienceToNextLevel;

        public HeroExp()
        {
            Level = 0;
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
            }
            
            OnExpChanged?.Invoke();
        }
    }
}
