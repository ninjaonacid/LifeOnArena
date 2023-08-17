using System.Collections.Generic;
using Code.Data;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class UIStatContainer : MonoBehaviour
    {
        [SerializeField] private List<StatsUI> StatSlots;
            
        private PlayerData _data;
        public void Construct(PlayerData data)
        {
            _data = data;
            
            InitializeStatsView();
        }

        private void InitializeStatsView()
        {
            var statsNames = new List<string>(_data.CharacterStatsData.StatsData.Keys);
            var statsValues = new List<int>(_data.CharacterStatsData.StatsData.Values);

            for (var index = 0; index < StatSlots.Count; index++)
            {
                var stat = StatSlots[index];
                stat.SetSlot(statsNames[index], statsValues[index]);
            }
        }
    }
}

