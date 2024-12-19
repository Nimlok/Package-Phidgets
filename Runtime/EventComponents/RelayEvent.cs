using Nimlok.Phidgets;
using Nimlok.Phidgets.EventComponents;
using UnityEngine;

namespace EventComponents
{
    public class RelayEvent: BasePhidgetEvent
    {
        [Space,SerializeField] private bool target;

        public void SetRelay(bool newTarget)
        {
            HubEventsManager.ActivatePhidget?.Invoke(newTarget, basePhidgetData.port, basePhidgetData.GetSerialNumber());
        }
        
        public void SetRelay()
        {
            HubEventsManager.ActivatePhidget?.Invoke(target, basePhidgetData.port, basePhidgetData.GetSerialNumber());
        }
    }
}