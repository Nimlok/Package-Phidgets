using Nimlok.Phidgets;
using Nimlok.Phidgets.EventComponents;
using UnityEngine;
using UnityEngine.Events;

namespace EventComponents
{
    public class RFIDVintEvent: InputPhidgetEvent
    {
        [SerializeField] private UnityEvent<RFIDData> onDigitalStateChange;
        
        protected override void AddListener()
        {
            ListenerToAdd(o => onDigitalStateChange?.Invoke((RFIDData)o));
        }

        protected override void RemoveListener()
        {
            ListenerToRemove(o => onDigitalStateChange?.Invoke((RFIDData)o));
        }
        
        public void SetAtennaEnable(bool enabled)
        {
            HubEventsManager.ActivatePhidget?.Invoke(enabled, basePhidgetData.port, basePhidgetData.GetSerialNumber());
        }
    }
}