using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Data
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