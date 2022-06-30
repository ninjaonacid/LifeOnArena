using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        public ItemIcon ItemIcon = null;

        private int _slotIndex;
        private HeroInventory _heroInventory;
        public void Setup(HeroInventory heroInventory, int slotIndex)
        {
            _slotIndex = slotIndex;
            _heroInventory = heroInventory;
            ItemIcon.SetItem(_heroInventory.GetItemInSlot(_slotIndex));
        }
    }
}
