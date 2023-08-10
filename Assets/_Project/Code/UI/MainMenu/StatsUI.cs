using TMPro;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        
        public void SetSlot(string statName, int value)
        {
            _text.text = statName + value;
        }
    }
}
