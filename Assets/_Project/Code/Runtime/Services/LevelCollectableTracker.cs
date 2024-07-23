namespace Code.Runtime.Services
{
    public class LevelCollectableTracker
    {
        public float GainedExperience { get; private set; }
        public int KilledEnemies { get; private set; }
        public int SoulsLoot { get; private set; }
        public float ObjectiveExperienceReward{ get; private set; }
        public float ObjectiveSoulsReward { get; private set; }
        
        public void CollectExperience(float value)
        {
            GainedExperience += value;
        }

        public void CollectKill()
        {
            KilledEnemies++;
        }

        public void CollectSouls(int souls)
        {
            SoulsLoot += souls;
        }

        public void SetObjectiveExperience(float value)
        {
            ObjectiveExperienceReward += value;
        }

        public void SetObjectiveSoulsReward(float value)
        {
            ObjectiveSoulsReward += value;
        }
    }
}
