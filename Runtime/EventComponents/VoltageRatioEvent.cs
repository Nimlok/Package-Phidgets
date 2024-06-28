using Phidgets.EventComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class VoltageRatioEvent: BasePhidgetEvent
    {
        [Space]
        [SerializeField] private UnityEvent<float> onVoltageChange;

        private void OnEnable()
        {
            PhidgetControllerEvents.AddListener?.Invoke(o => onVoltageChange?.Invoke((float)o), basePhidgetData.port, basePhidgetData.GetSerialNumber()); 
        }
        
        private void OnDisable()
        {
            PhidgetControllerEvents.RemoveListener?.Invoke(o => onVoltageChange?.Invoke((float)o), basePhidgetData.port, basePhidgetData.GetSerialNumber()); 
        }
    }
}