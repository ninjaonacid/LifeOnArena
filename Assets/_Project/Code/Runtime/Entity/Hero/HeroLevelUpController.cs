using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;

namespace Code.Runtime.Entity.Hero
{
    public class HeroLevelUpController : MonoBehaviour, ISave
    {
        [SerializeField] private HeroStats _stats;
        [SerializeField] private VisualEffectController _vfxController;
        private int _heroLevel;
        public void LoadData(PlayerData data)
        {
            data.PlayerExp.OnLevelChanged += HandleLevelUp;
        }

        private void HandleLevelUp()
        {
            var stats = _stats.Stats;
            
            foreach (var stat in stats.Values)
            {
                if (stat is Health attribute)
                {
                    attribute.Add(attribute.StatPerLevel);
                } 
                else if (stat is PrimaryStat primaryStat)
                {
                    primaryStat.Add(primaryStat.StatPerLevel);
                }
            }
        }

        public void UpdateData(PlayerData data)
        {
           
        }
    }
}