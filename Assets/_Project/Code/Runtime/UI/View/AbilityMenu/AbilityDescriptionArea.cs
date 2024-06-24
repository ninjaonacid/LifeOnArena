using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace Code.Runtime.UI.View.AbilityMenu
{
    public class AbilityDescriptionArea : MonoBehaviour
    {
        public LocalizeStringEvent LocalizeString;
        public TextMeshProUGUI Text;
        public Image Icon;
        public CanvasGroup CanvasGroup;

        public void Show(bool value)
        {
            CanvasGroup.alpha = value ? 1 : 0;
        } 
    }
}
