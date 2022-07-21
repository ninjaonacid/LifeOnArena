using CodeBase.UI.Inventory;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "NewArmor", menuName = "StaticData/Armor")]
    public class ArmorData : InventoryItem, IEquipable
    {
        public int Defence;
        public int HPbonus;

        public EquipmentSlot EquipmentSlot;
        public EquipmentSlot GetEquipmentSlot()
        {
            return EquipmentSlot;
        }
    }
}
