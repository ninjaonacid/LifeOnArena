using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class ResourcesCountUI : MonoBehaviour
    {
        [SerializeField] private Image _resourceImage;
        [SerializeField] private TextMeshProUGUI _text;

        public void ChangeText(string text)
        {
            _text.text = text;
        }
    }
}
