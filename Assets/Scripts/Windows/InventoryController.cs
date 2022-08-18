using Assets.Scripts.Model.Data;
using UnityEngine;
namespace Assets.Scripts
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;
        [SerializeField] private Transform _draggingParent;

        private GameSession _session;
        private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(_prefab, _container);
            _trash.Retain(_session.InventoryModel.Subscribe(Rebuilt));
            Rebuilt();
        }

        private void Rebuilt()
        {
            var inventory = _session.InventoryModel.Inventory;
            _dataGroup.SetData(inventory);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
