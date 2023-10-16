using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.View.AbilityMenu
{
    public class AbilityItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _abilityIcon;
        [SerializeField] private Image _selectionFrame;
        [SerializeField] private TextMeshProUGUI _equippedSlotIndex; 

        public event Action<AbilityItemView> OnAbilityItemClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnAbilityItemClick?.Invoke(this);
        }

        private void Awake()
        {
            _equippedSlotIndex.color = new Color(1, 1, 0);
        }

        public void SetData(Sprite icon, int equippedSlotIndex = 0)
        {
            _abilityIcon.sprite = icon;

            if (equippedSlotIndex == 0)
            {
                _equippedSlotIndex.gameObject.SetActive(false);
            }
            else
            {
                _equippedSlotIndex.gameObject.SetActive(true);
                _equippedSlotIndex.text = equippedSlotIndex.ToString();
                
            }
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
