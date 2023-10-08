using Code.ConfigData.StatSystem;
using Code.Data;
using Code.Data.PlayerData;
using Code.Services.PersistentProgress;

namespace Code.Entity.Hero
{
    public class HeroStats : StatController, ISave
    {
        private StatsData _statsData;

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.T))
        //     {
        //         Attribute health = _stats["Health"] as Attribute;
        //         health.ApplyModifier(new StatModifier()
        //         {
        //             OperationType = ModifierOperationType.Additive,
        //             Magnitude = -10,
        //             Source = this,
        //         });
        //         Debug.Log(health.BaseValue.ToString());
        //     }
        // }

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
            
            StatsLoaded();
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
