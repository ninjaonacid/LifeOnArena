using CodeBase.Hero;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        public InventoryItemIcon inventoryItemIcon = null;

        private int _slotIndex;
        private HeroInventory _heroInventory;
        private Image _inventoryBackground;
        private ItemPreviewUI _itemPreviewUI;

        private void Start()
        {
            _inventoryBackground = GetComponent<Image>();
            _inventoryBackground.enabled = true;
        }

        public void Setup(HeroInventory heroInventory, 
                            ItemPreviewUI itemPreviewUI,
                            int slotIndex)
        {
            _heroInventory = heroInventory;
            _itemPreviewUI = itemPreviewUI;
            _slotIndex = slotIndex;
            inventoryItemIcon.SetItem(_heroInventory.GetItemInSlot(_slotIndex));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           _itemPreviewUI.SetItemInfo(GetItem());
        }

        public InventoryItem GetItem()
        {
            return _heroInventory.GetItemInSlot(_slotIndex);
        }
    }
}
