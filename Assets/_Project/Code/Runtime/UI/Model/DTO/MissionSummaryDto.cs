namespace Code.Runtime.UI.Model.DTO
{
    public class MissionSummaryDto : IScreenModelDto

    {
        public float ExpEarnedFromEnemies;
        public int EnemiesKilled;
        public float ExpForClearLevel;

        public MissionSummaryDto(float expEarnedFromEnemies, int enemiesKilled, float expForClearLevel)
        {
            ExpEarnedFromEnemies = expEarnedFromEnemies;
            EnemiesKilled = enemiesKilled;
            ExpForClearLevel = expForClearLevel;
        }
    }
}