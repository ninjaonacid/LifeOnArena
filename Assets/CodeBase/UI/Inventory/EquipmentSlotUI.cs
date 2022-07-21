using CodeBase.Hero;
using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Inventory
{
    public class EquipmentSlotUI : MonoBehaviour, IPointerClickHandler
    {
        public EquipmentSlot equipmentSlot;
        public InventoryItemIcon icon;

        private ItemPreviewUI _itemPreview;
        private HeroEquipment _heroEquipment;


        public void Construct(HeroEquipment heroEquipment, ItemPreviewUI itemPreview)
        {
            _heroEquipment = heroEquipment;
            _itemPreview = itemPreview;
            _heroEquipment.OnEquipmentChanged += RedrawUI;
        }

        private void RedrawUI()
        {
            icon.SetItem((InventoryItem)_heroEquipment.GetItemInSlot(equipmentSlot));
        }

        public void AddItemToSlot(IEquipable item)
        {
            if (item is IEquipable equipable)
            {
                _heroEquipment.EquipItem(equipmentSlot, equipable);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _itemPreview.SetItemInfo(GetEquipmentItem() as InventoryItem);
        }

        public IEquipable GetEquipmentItem() =>
            _heroEquipment.GetItemInSlot(equipmentSlot);
        
        
    }
}










































 

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                            
