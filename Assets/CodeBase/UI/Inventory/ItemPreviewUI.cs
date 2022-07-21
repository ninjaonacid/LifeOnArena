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
        public InventoryItem SelectedItem { get; set; }

        public void SetItemInfo(InventoryItem item)
        {
            SelectedItem = item;
            if (item is WeaponData)
            {
                var weapon = item as WeaponData;
                TextMeshPro.text = "AttackRadius " + weapon.AttackRadius.ToString() + 
                                   "\n" + 
                                    "AttackDamage " + weapon.AttackSpeed.ToString();
                ItemPreViewIcon.sprite = item.GetIcon();
            }
        }

        public InventoryItem GetSelectedItem() => SelectedItem;

        
    }
}
