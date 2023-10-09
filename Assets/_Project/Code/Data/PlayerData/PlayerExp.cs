using System;

namespace Code.Data.PlayerData
{
    [Serializable]
    public class PlayerExp
    {
        public event Action OnExpChanged;
        public int Experience;
        public int Level;
        public int ExperienceToNextLevel;

        public PlayerExp()
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
