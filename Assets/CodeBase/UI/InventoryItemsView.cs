using System;
using CodeBase.Hero;
using CodeBase.Infrastructure.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class InventoryItemsView : MonoBehaviour
    {
        private HeroInventory _heroInventory;
        private IGameFactory _gameFactory;
        public void Construct(HeroInventory heroInventory, IGameFactory gameFactory)

        {
            _gameFactory =  gameFactory;
            _heroInventory = heroInventory;
           
        }
        
        private void Start()
        {
            _heroInventory.OnInventoryChanged += Redraw;
            Redraw();
        }


        public void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < _heroInventory.GetInventorySize(); i++)
            {
                InventorySlotUI inventoryItem =
                    _gameFactory
                        .CreateInventorySlot(transform)
                        .GetComponent<InventorySlotUI>();
                
                inventoryItem.Setup(_heroInventory, i);
            }
        }
    }
}
