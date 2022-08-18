using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        public InventoryModel InventoryModel { get; private set; }

        private void Awake()
        {
            if(IsSessionExists())
                Destroy(gameObject);

            else
            {
                InventoryModel = new InventoryModel(Data);
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExists()
        {
            var session = FindObjectsOfType<GameSession>();
            foreach (var gameSession in session)
            {
                if (gameSession != this)
                    return true;

            }
            return false;
        }

    }
}
