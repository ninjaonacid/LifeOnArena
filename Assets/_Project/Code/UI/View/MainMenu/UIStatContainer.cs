using System.Collections.Generic;
using Code.Data;
using Code.UI.MainMenu;
using UnityEngine;

namespace Code.UI.View.MainMenu
{
    public class UIStatContainer : MonoBehaviour
    {
        [SerializeField] private List<StatsUI> StatSlots;
        [SerializeField] private StatsUI Health;
        [SerializeField] private StatsUI Attack;
        [SerializeField] private StatsUI Defense;
        
        public void SetHealth(string statname, int value)
        {
            Health.SetSlot(statname, value);
        }

        public void SetAttack(string statName, int value)
        {
            Attack.SetSlot(statName, value);
        }

        public void SetDefense(string statName, int value)
        {
            Defense.SetSlot(statName, value);
        }
        
        
      
    }
}

