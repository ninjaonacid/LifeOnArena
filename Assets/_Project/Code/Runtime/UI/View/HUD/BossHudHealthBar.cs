using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.HUD
{
    public class BossHudHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TextMeshProUGUI _bossName;
        [SerializeField] private CanvasGroup _canvasGroup;

        public void UpdateHpBar(float value, float currentValue)
        {
            _healthBar.fillAmount = currentValue / value;
        }

        public void SetBossName(string text)
        {
            _bossName.text = text;
        }

        public void Show(bool value)
        {
            _canvasGroup.alpha = value ? _canvasGroup.alpha = 1 : _canvasGroup.alpha = 0;
        }
    }
}
