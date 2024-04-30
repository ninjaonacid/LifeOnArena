using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.HUD
{
    public class StatusBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarImage;
        [SerializeField] private Image _expBarImage;
        [SerializeField] private TextMeshProUGUI _lvl;
        

        public void SetHpValue(float current, float max)
        {
            _hpBarImage.fillAmount = current / max;
        }

        public void SetExpValue(float current, float max)
        {
            _expBarImage.fillAmount = current / max;
        }

        public void SetLevel(int value)
        {
            _lvl.text = value.ToString();
        }
    }
}