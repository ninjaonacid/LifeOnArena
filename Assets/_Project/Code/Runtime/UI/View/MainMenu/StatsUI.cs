using Code.Runtime.UI.Buttons;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.View.MainMenu
{
    public class StatsUI : MonoBehaviour
    {
        [SerializeField] private BaseButton _plusButton;
        [SerializeField] private TextMeshProUGUI _statName;
        [SerializeField] private TextMeshProUGUI _statValue;

        public void SetStatValue(int value)
        {
            _statValue.SetText(value.ToString());
        }
    }
}
