using System;
using System.Collections.Generic;
using Code.StaticData;

namespace Code.Data
{
    [Serializable]
    public class InventoryData
    {
        public List<InventoryItem> InventoryItems;
        public int ItemAmount = 1;
        public InventoryData()
        {
            InventoryItems = new List<InventoryItem>();
        }
    }
}