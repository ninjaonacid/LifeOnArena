using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using Code.Runtime.Services.PersistentProgress;

namespace Code.Runtime.Entity.Hero
{
    public class HeroStats : StatController, ISave
    {
        private StatsData _statsData;

        public void LoadData(PlayerData data)
        {
            _statsData = data.StatsData;

            foreach (var stat in Stats.Values)
            {
                if (stat is ISave saveable)
                {
                    saveable.LoadData(data);
                }
            }
            
            StatsInitialized();
        }

        public void UpdateData(PlayerData data)
        {
            foreach (var stat in Stats.Values)
            {
                if (stat is ISave saveable)
                {
                    saveable.UpdateData(data);
                }
            }
        }
    }
}
