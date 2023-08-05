using TMPro;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        

        private void Awake()
        {
            
        }

        public void SetSlot(string statName, int value)
        {
            _text.text = statName + value;

        }
    }
}
