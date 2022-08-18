using Assets.Scripts.Model.Data.Properties;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public InventoryData Inventory => _inventory;

        public FloatProperty Gold;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
