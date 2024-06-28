using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.EventComponents
{
    public class DigitalInputEvent : BasePhidgetEvent
    {
        [SerializeField] private UnityEvent<bool> onDigitalStateChange;
        
        private void OnEnable()
        {
            PhidgetControllerEvents.AddListener?.Invoke(o => onDigitalStateChange?.Invoke((bool)o), basePhidgetData.port, basePhidgetData.GetSerialNumber()); 
        }

        private void OnDisable()
        {
           PhidgetControllerEvents.RemoveListener?.Invoke(o => onDigitalStateChange?.Invoke((bool)o), basePhidgetData.port, basePhidgetData.GetSerialNumber()); 
        }
    }
}

