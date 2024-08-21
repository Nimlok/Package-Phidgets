using Nimlok.Phidgets.EventComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Nimlok.Phidgets.Events
{
    public class VoltageRatioEvent: InputPhidgetEvent
    {
        [Space]
        [SerializeField] private UnityEvent<float> onVoltageChange;
        
        protected override void AddListener()
        {
            ListenerToAdd(o => onVoltageChange?.Invoke((float)o));
        }

        protected override void RemoveListener()
        {
            ListenerToRemove(o => onVoltageChange?.Invoke((float)o));
        }
    }
}