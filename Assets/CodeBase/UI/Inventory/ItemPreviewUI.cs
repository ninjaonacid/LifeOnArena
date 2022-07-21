using System.Reflection.Emit;
using CodeBase.Hero;
using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeaponData = CodeBase.StaticData.WeaponData;

namespace CodeBase.UI.Inventory
{
    public class ItemPreviewUI : MonoBehaviour
    {
        public Image ItemPreViewIcon;
        public TextMeshProUGUI TextMeshPro;


        public EquipmentItems equipmentItems;
        private InventoryItem _selectedItem { get; set; }


        public void Construct(HeroEquipment heroEquipment)
        {
            //_heroEquipment = heroEquipment;
        }

        public void SetItemInfo(InventoryItem item)
        {
            _selectedItem = item;
            if (item is WeaponData)
            {
                var weapon = item as WeaponData;
                TextMeshPro.text = weapon.AttackRadius.ToString() + 
                                   "\n" + 
                                   weapon.AttackSpeed.ToString();
                ItemPreViewIcon.sprite = item.GetIcon();
            }
        }

        public void EquipItem()
        {
            if (_selectedItem is IEquipable)
            {
                var equipableItem = _selectedItem as IEquipable;

                equipmentItems.SetEquippedItem(equipableItem, equipableItem.GetEquipmentSlot());
            }
        }

        public InventoryItem GetSelectedItem() => _selectedItem;

        
    }
}
