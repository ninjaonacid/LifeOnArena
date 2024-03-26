using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.AbilityMenu
{
    public class AbilityTreeCell : MonoBehaviour
    {
        [SerializeField] private Image _abilitySlotFrame;

        private void Start()
        {
            _abilitySlotFrame.transform.SetAsLastSibling();
        }
    }
}
