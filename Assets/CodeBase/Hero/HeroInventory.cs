using System;
using CodeBase.Data;
using CodeBase.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroInventory : MonoBehaviour, ISavedProgress
    {
        private int inventorySize = 20;

        private InventorySlot[] inventorySlots;

        public struct InventorySlot
        {
            public InventoryItem item;
            public int amount;
        }

        public event Action OnInventoryChanged;
        public static HeroInventory Instance;


        private void Awake()
        {
            Instance = this;
            inventorySlots = new InventorySlot[inventorySize];
        }

        public void RemoveItemFromSlot(int slot, int amount)
        {

            inventorySlots[slot].amount -= amount;

            if (inventorySlots[slot].amount <= 0)
            {
                inventorySlots[slot].amount = 0;
                inventorySlots[slot].item = null;
            }

            OnInventoryChanged?.Invoke();
        }

        public InventoryItem GetItemInSlot(int slot) => inventorySlots[slot].item;

        public int GetAmountInSlot(int slot) => inventorySlots[slot].amount;

        public int GetInventorySize() => inventorySlots.Length;

        public bool AddItemToEmptySlot(InventoryItem item, int amount)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            inventorySlots[i].item = item;
            inventorySlots[i].amount = amount;
            
            OnInventoryChanged?.Invoke();

            return true;
        }

        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        private int FindSlot(InventoryItem item)
        {
            int i = FindStack(item);

            if (i < 0)
            {
                i = FindEmptySlot();
            }

            return i;
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].item == null)
                {
                    return i;
                }
            }

            return -1;
        }

        private int FindStack(InventoryItem item)
        {
            if (!item.IsStackable())
            {
                return -1;
            }

            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (object.ReferenceEquals(inventorySlots[i].item, item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            for (int i = 0; i < progress.InventoryData.InventoryItems.Count; i++)
            {
                inventorySlots[i].item = progress.InventoryData.InventoryItems[i];
            }
            progress.InventoryData.InventoryItems.Clear();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                progress.InventoryData.InventoryItems.Add(inventorySlots[i].item);
            }
        }
    }
}
