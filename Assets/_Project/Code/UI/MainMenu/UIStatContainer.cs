using System;
using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class UIStatContainer : MonoBehaviour
    {
        [SerializeField] private List<StatsUI> StatSlots;
            
        private PlayerProgress _progress;
        public void Construct(PlayerProgress progress)
        {
            _progress = progress;
            
            InitializeStatsView();
        }

        private void InitializeStatsView()
        {
            var statsNames = new List<string>(_progress.CharacterStatsData.StatsData.Keys);
            var statsValues = new List<int>(_progress.CharacterStatsData.StatsData.Values);

            for (var index = 0; index < StatSlots.Count; index++)
            {
                var stat = StatSlots[index];
                stat.SetSlot(statsNames[index], statsValues[index]);
            }
        }
    }
}

