using Assets.Scripts.Model.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventoryItemWidget : MonoBehaviour, IItemRenderer<InventoryItemData>, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _id;
        [SerializeField] private Text _price;
        [SerializeField] private Text _count;

        private GameSession _session;
        private Transform _draggingParent;
        private Transform _originalParent;

        private InventoryItemData _item;
        private float _sellPrice;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void SetData(InventoryItemData item, int index)
        {
            _item = item;
            var itemDef = DefsFacade.Instance.Items.Get(item.Id);
            _sellPrice = itemDef.Price * 0.8f;

            _icon.sprite = itemDef.Icon;
            _id.text = item.Id;
            _price.text = _sellPrice.ToString();

            _count.text = item.Value.ToString();
            Init(transform.parent.parent);
        }

        public void Init(Transform draggingParent)
        {
            _draggingParent = draggingParent;
            _originalParent = transform.parent;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            int closestIndex = 0;
            for (int i = 0; i < _originalParent.transform.childCount; i++)
            {
                if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) < Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
                {
                    closestIndex = i;
                }
            }

            transform.SetParent(_originalParent);
            transform.SetSiblingIndex(closestIndex);

            if (!In((RectTransform)_originalParent))
            {
                Sell();
            }
        }

        private void Sell()
        {
            _session.Data.Gold.Value += _sellPrice;
            _session.Data.Inventory.Remove(_item.Id, 1);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_draggingParent);
        }

        private bool In(RectTransform originalParent)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
        }
    }
}
