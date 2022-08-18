using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class InventoryModel : IDisposable
    {
        private readonly PlayerData _data;

        public InventoryItemData[] Inventory { get; private set; }

        public event Action OnChanged;

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public InventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetAll();
            _data.Inventory.OnChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            Inventory = _data.Inventory.GetAll();
            OnChanged?.Invoke();
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged += OnInventoryChanged;
        }
    }
}
