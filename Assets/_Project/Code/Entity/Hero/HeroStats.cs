using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData.StatSystem;

namespace Code.Entity.Hero
{
    public class HeroStats : StatController, ISave
    {
        private CharacterStatsData _characterStatsData;

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
            _characterStatsData = data.CharacterStatsData;

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
