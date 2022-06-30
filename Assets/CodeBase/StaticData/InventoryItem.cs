using CodeBase.Logic;
using JetBrains.Annotations;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "StaticData/Item")]
    public class InventoryItem : ScriptableObject
    {
        [SerializeField] private ItemTypeId ItemTypeId = ItemTypeId.Default;
        [SerializeField] private string DisplayName = null;
        [SerializeField] private int Amount;
        [SerializeField] private GameObject ItemDropPrefab;
        [SerializeField] private Sprite ItemIcon;
        [SerializeField] private bool IsStackableItem = false;


        public bool IsStackable() => IsStackableItem;

        public Sprite GetIcon() => ItemIcon;


    }
}
