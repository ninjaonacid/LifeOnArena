using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.UI.View.AbilityMenu
{
    public class AbilityItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private Image _selectionFrame;

        public event Action<AbilityItemView> OnAbilityItemClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnAbilityItemClick?.Invoke(this);
        }

        public void Select()
        {
            _selectionFrame.enabled = true;
        }

        public void Deselect()
        {
            _selectionFrame.enabled = false;
        }
    }
}
