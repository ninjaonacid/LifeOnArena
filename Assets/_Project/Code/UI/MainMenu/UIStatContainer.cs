using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class UIStatContainer : MonoBehaviour
    {
        [SerializeField] private List<StatsUI> StatSlots;
        [SerializeField] private StatsUI Health;
        [SerializeField] private StatsUI Attack;
        [SerializeField] private StatsUI Defense;
            
        private PlayerData _data;
        public void Construct(PlayerData data)
        {
            _data = data;
            
           // InitializeStatsView();
        }

        public void SetHealth(string statname, int value)
        {
            Health.SetSlot(statname, value);
        }

        public void SetAttack(string statName, int value)
        {
            Attack.SetSlot(statName, value);
        }
        private void InitializeStatsView()
        {
            // var statsNames = new List<string>(_data.StatsData.Stats.Keys);
            // var statsValues = new List<int>(_data.StatsData.Stats.Values);
            //
            // for (var index = 0; index < StatSlots.Count; index++)
            // {
            //     var stat = StatSlots[index];
            //     stat.SetSlot(statsNames[index], statsValues[index]);
            // }
        }
    }
}

