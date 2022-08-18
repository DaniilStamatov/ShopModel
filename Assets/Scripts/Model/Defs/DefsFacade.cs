using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]

    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;

        public InventoryItemDef Items => _items;

        private static DefsFacade _instance;
        public static DefsFacade Instance => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}
