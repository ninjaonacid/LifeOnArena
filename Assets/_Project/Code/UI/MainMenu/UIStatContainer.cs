using System;
using System.Collections.Generic;
using System.Linq;
using Code.Hero;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class UIStatContainer : MonoBehaviour
    {
        private List<StatsUI> _stats = new List<StatsUI>();
        private HeroStats _heroStats;
        public void Construct(HeroStats heroStats)
        {
            _heroStats = heroStats;
        }
        
        private void Awake()
        {
            var stats = GetComponentsInChildren<StatsUI>();
            _stats.AddRange(stats);

            
        }

        private void Start()
        {
            var statName = _heroStats.Stats.Keys.ToList();
            var statValue = _heroStats.Stats.Values.ToList();

            for (var index = 0; index < _stats.Count; index++)
            {
                var stat = _stats[index];
                
                stat.SetSlot(statName[index], statValue[index].Value);
            }
        }
    }
}

