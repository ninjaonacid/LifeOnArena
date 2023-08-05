using System.Collections.Generic;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class StatContainer : MonoBehaviour
    {
        private List<StatsUI> _stats;

        private void Awake()
        {
            var stats = GetComponentsInChildren<StatsUI>();
            _stats.AddRange(stats);

            foreach (var stat in _stats)
            {
                
            }
        }
    }
}
