using Assets.Scripts.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Windows
{
    public class ShopItemWidget : MonoBehaviour, IItemRenderer<ItemDef>, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _id;
        [SerializeField] private Text _price;

        private ItemDef _data;
        
        private Transform _draggingParent;
        private Transform _originalParent;
        private int _index;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }
        public void SetData(ItemDef data, int index)
        {
            _data = data;
            _icon.sprite = data.Icon;
            _id.text = data.Id;
            _price.text = data.Price.ToString();
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
            if (!In((RectTransform)_originalParent))
            {
                Buy();
            }
            
            transform.SetParent(_originalParent);
            transform.SetSiblingIndex(_index);
        }

        private void Buy()
        {
            if (_session.Data.Gold.Value < _data.Price)
            {
                Debug.Log("Not enough gold!");
            }
            else
            {
                _session.Data.Inventory.Add(_data.Id, 1);
                _session.Data.Gold.Value -= _data.Price;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _index = transform.GetSiblingIndex();
            transform.SetParent(_draggingParent);
        }

        private bool In(RectTransform originalParent)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
        }
    }
}
