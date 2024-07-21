namespace Code.Runtime.UI.Model.DTO
{
    public class MissionSummaryDto : IScreenModelDto
    {
        public readonly float ExpEarnedFromEnemies;
        public readonly int EnemiesKilled;
        public readonly float ExpForClearLevel;
        public readonly int SoulsLoot;

        public MissionSummaryDto(float expEarnedFromEnemies, int enemiesKilled, int soulLoot, float expForClearLevel)
        {
            ExpEarnedFromEnemies = expEarnedFromEnemies;
            EnemiesKilled = enemiesKilled;
            SoulsLoot = soulLoot;
            ExpForClearLevel = expForClearLevel;
        }
    }
}