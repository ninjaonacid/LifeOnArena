namespace Code.Runtime.Services
{
    public class LevelCollectableTracker
    {
        public float GainedExperience { get; private set; }
        public int KilledEnemies { get; private set; }
        public int SoulsLoot { get; private set; }


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
    }
}
