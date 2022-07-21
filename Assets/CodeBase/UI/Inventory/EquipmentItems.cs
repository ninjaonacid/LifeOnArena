using CodeBase.Hero;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Inventory
{
    public class EquipmentItems : MonoBehaviour
    {
        private HeroEquipment _heroEquipment;

        public EquipmentSlotUI Head;
        public EquipmentSlotUI Chest;
        public EquipmentSlotUI Weapon;
        public EquipmentSlotUI Boots;
        public void Construct(HeroEquipment heroEquipment)
        {
            _heroEquipment = heroEquipment;
        }

        public void SetEquippedItem(IEquipable item, EquipmentSlot equipmentSlot)
        {
 
            switch (equipmentSlot)
            {
                case EquipmentSlot.Helm:
                {   
                    Head.AddItemToSlot(item);
                    break;
                }
                case EquipmentSlot.Body:
                {   
                    Chest.AddItemToSlot(item);
                    break;
                }
                case EquipmentSlot.Weapon:
                {   
                    _heroEquipment.EquipWeapon(item as WeaponData);
                    Weapon.AddItemToSlot(item);
                    break;
                }
                case EquipmentSlot.Boots:
                {   
                    Boots.AddItemToSlot(item);
                    break;
                }
            }
        }
    }
}