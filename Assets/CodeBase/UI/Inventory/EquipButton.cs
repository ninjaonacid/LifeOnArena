using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Inventory
{
    public class EquipButton : MonoBehaviour, IPointerClickHandler
    {
        public ItemPreviewUI itemPreview;
        public EquipmentItems EquipmentItems;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var selectedItem = itemPreview.SelectedItem;

            if (selectedItem is IEquipable)
            {
                var equipableItem = selectedItem as IEquipable;

                EquipmentItems.SetEquippedItem(equipableItem, equipableItem.GetEquipmentSlot());
            }
        }
    }
}
