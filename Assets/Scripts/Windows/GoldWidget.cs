using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Windows
{
    public class GoldWidget : MonoBehaviour
    {
        [SerializeField] private Text _goldCount;


        private GameSession _session;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Gold.Subscribe(SetGoldCount);
            SetGoldCount(_session.Data.Gold.Value, 0);
        }

        private void SetGoldCount(float newValue, float _)
        {
            _goldCount.text = newValue.ToString();
        }

    }
}
