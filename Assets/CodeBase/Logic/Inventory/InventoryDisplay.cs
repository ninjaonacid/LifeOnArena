using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.UI.Inventory;
using UnityEngine;


namespace CodeBase.Logic.Inventory
{
    public class InventoryDisplay : MonoBehaviour
    {
        public InventoryItemsView InventoryItemsView;
        public EquipmentItems equipmentItems;
        public ItemPreviewUI ItemPreviewUI;

        public GameObject InventoryContainer;

        private IInputService _input;

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            InventoryContainer.SetActive(false);
        }

        private void Update()
        {
            ShowHideInventory();
        }

        private void ShowHideInventory()
        {
            if (_input.InventoryButton())
            {
                InventoryContainer.SetActive(!InventoryContainer.activeSelf);
            }
        }
    }
}
