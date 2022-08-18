using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public event Action<string, int> OnChanged;
    public void Add(string id, int value)
    {
        if (value <= 0) return;

        var itemDef = DefsFacade.Instance.Items.Get(id);
        if (itemDef.IsVoid) return;
        var item = GetItem(id);
        if (item == null)
        {
            item = new InventoryItemData(id);
            _inventory.Add(item);
        }

        item.Value += value;

        OnChanged?.Invoke(id, Count(id));
    }

    public void Remove(string id, int value)
    {
        var item = GetItem(id);
        if (item == null) return;

        if (item.Value > 0)
            item.Value -= value;

        if (item.Value <= 0)
            _inventory.Remove(item);

        OnChanged?.Invoke(id, Count(id));
    }


    private InventoryItemData GetItem(string id)
    {
        foreach (var item in _inventory)
        {
            if (item.Id == id) return item;
        }
        return null;
    }

    public int Count(string id)
    {
        var count = 0;
        foreach (var item in _inventory)
        {
            if (item.Id == id)
            {
                count += item.Value;
            }
        }
        return count;
    }

    public InventoryItemData[] GetAll()
    {
        var returnValue = new List<InventoryItemData>();
        foreach (var item in _inventory)
        {
            returnValue.Add(item);
        }
        return returnValue.ToArray();
    }
}


[Serializable]
public class InventoryItemData
{
    public string Id;
    public int Value;

    public InventoryItemData(string id)
    {
        Id = id;
    }
}