using Phidgets.EventComponents;
using UnityEngine;

namespace Phidgets.Events
{
    public class StepMotorEvents: BasePhidgetEvent
    {
        [Space,SerializeField] private float target;

        public void SetMotorPosition(float targetPosition)
        {
            HubEventsManager.ActivatePhidget?.Invoke(targetPosition, basePhidgetData.port, basePhidgetData.GetSerialNumber());
        }
        
        public void SetMotorPosition()
        {
            HubEventsManager.ActivatePhidget?.Invoke(target, basePhidgetData.port, basePhidgetData.GetSerialNumber());
        }
    }
}