using CodeBase.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Inventory
{
    [RequireComponent(typeof(Image))]
    public class InventoryItemIcon : MonoBehaviour
    {
       
        public void SetItem(InventoryItem item)
        {
            var icon = gameObject.GetComponent<Image>();
            if (item == null)
            {
                icon.enabled = false;
            }
            else
            {
                icon.enabled = true;
                icon.sprite = item.GetIcon();
            }
        }

        public void SetItem(InventoryItem item, int amount)
        {

        }
    }
}
