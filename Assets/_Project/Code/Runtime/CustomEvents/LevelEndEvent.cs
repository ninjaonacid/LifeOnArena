namespace Code.Runtime.CustomEvents
{
    public struct LevelEndEvent : IEvent
    {
        public float ExpForEnemies;
        public float ExpForLevel;
        public int TotalKilledEnemies;

        public LevelEndEvent(float expForEnemies, float expForLevel, int totalKilledEnemies)
        {
            ExpForEnemies = expForEnemies;
            ExpForLevel = expForLevel;
            TotalKilledEnemies = totalKilledEnemies;
        }
    }
}