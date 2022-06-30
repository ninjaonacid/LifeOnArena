using System;
using CodeBase.Hero;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Logic.Inventory
{
    public class InventoryDisplay : MonoBehaviour
    {
        public InventorySlotUI inventorySlotPrefab;

        private HeroInventory _heroInventory;
        private void Start()
        {
            _heroInventory = HeroInventory.Instance;
        }

        public void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child);
            }

            for (int i = 0; i < _heroInventory.GetInventorySize(); i++)
            {
                InventorySlotUI inventoryItem =
                    Instantiate(inventorySlotPrefab, transform);
                inventoryItem.Setup(_heroInventory, i);
            }
        }
    }
}
