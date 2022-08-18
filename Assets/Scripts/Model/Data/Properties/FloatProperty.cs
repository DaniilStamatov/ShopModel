using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model.Data.Properties
{
    [Serializable]
    public class FloatProperty 
    {
            [SerializeField] protected float _value;

            public delegate void OnPropertyChanged(float newValue, float oldValue);

            public event OnPropertyChanged OnChanged;

            public IDisposable Subscribe(OnPropertyChanged call)
            {
                OnChanged += call;
                return new ActionDisposable(() => OnChanged -= call);
            }
            
            public float Value
            {
                get => _value;
                set
                {
                    var isSame = _value.Equals(value);
                    if (isSame) return;
                    var oldValue = _value;
                    _value = value;
                    InvokeChangedEvent(_value, oldValue);
                }
            }

            private void InvokeChangedEvent(float newValue, float oldValue)
            {
                OnChanged?.Invoke(newValue, oldValue);
            }
        }
    }

