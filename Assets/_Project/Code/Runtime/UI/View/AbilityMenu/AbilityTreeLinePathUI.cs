using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityTreeLinePathUI : MonoBehaviour
    {
        [SerializeField] private Image _lineImage;
        [SerializeField] private Color32 _lockedColor;
        [SerializeField] private Color32 _unlockedColor;

        public void ChangeColor(bool isUnlocked)
        {
            if (_lineImage != null)
            {
                _lineImage.color = isUnlocked ? _unlockedColor : _lockedColor;
            }
        }
    }
}