using Assets.Scripts.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.Scripts.Windows
{
    public class ShopController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private ShopItemWidget _prefab;

        private DataGroup<ItemDef, ShopItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<ItemDef, ShopItemWidget>(_prefab, _container);
            SetShop();
        }

        private void SetShop()
        {
            _dataGroup.SetData(DefsFacade.Instance.Items.All);
        }
    }
}
