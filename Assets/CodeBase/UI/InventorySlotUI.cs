using System;
using CodeBase.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        public InventoryItemIcon inventoryItemIcon = null;

        private int _slotIndex;
        private HeroInventory _heroInventory;
        private Image _inventoryBackground;

        private void Start()
        {
            _inventoryBackground = GetComponent<Image>();
            _inventoryBackground.enabled = true;
        }

        public void Setup(HeroInventory heroInventory, int slotIndex)
        {
            _slotIndex = slotIndex;
            _heroInventory = heroInventory;
            inventoryItemIcon.SetItem(_heroInventory.GetItemInSlot(_slotIndex));
        }
    }
}
