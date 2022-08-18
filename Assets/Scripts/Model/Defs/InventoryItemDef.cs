using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]

    public class InventoryItemDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;
            foreach (var item in _items)
            {
                if (item.Id == id)
                    return item;
            }
            return default;
        }

        public ItemDef[] All => new List<ItemDef>(_items).ToArray();

    }

    [Serializable]

    public struct ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;

        [Space]
        [SerializeField] private int _price;


        public string Id => _id;
        public int Price => _price;

        public bool IsVoid => string.IsNullOrEmpty(_id);
        public Sprite Icon => _icon;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [SerializeField] private string _itemId;
        [SerializeField] private int _count;

        public string ItemId => _itemId;
        public int Count => _count;
    }
}