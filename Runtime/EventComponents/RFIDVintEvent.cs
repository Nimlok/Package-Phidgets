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
        
        public void SetAntennaEnable(bool enabled)
        {
            HubEventsManager.ActivatePhidget?.Invoke(enabled, basePhidgetData.port, basePhidgetData.GetSerialNumber());
        }

        public string GetTagPresent()
        {
            var phidget = basePhidgetData.hub.FindPhidget(basePhidgetData.port);
            if (phidget == null)
            {
                Debug.LogError($"Failed to get phidget for tag: {basePhidgetData.port}");
                return string.Empty;
            }
            
            var rfidvInt = (RFIDVint)phidget;
            return rfidvInt.GetBasePhidget.TagPresent ? rfidvInt.GetBasePhidget.GetLastTag().TagString : string.Empty;
        }

        public Phidget22.RFID GetRFIDPhidget()
        {
            var phidget = basePhidgetData.hub.FindPhidget(basePhidgetData.port);
            if (phidget == null)
            {
                Debug.LogError($"Failed to get phidget for tag: {basePhidgetData.port}");
                return null;
            }
            
            var rfidvInt = (RFIDVint)phidget;
            return rfidvInt.GetBasePhidget;
        }
    }
}