using Phidgets.EventComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class AccelerometerEvent : BasePhidgetEvent
    {
        [SerializeField] private UnityEvent<Vector3> OnAccelerometerChange;

        private void OnEnable()
        {
            PhidgetControllerEvents.AddListener?.Invoke((soundLevel) => OnAccelerometerChange?.Invoke((Vector3)soundLevel), basePhidgetData.port, basePhidgetData.hubSerialNumber);
        }

        private void OnDisable()
        {
            PhidgetControllerEvents.RemoveListener?.Invoke((soundLevel) => OnAccelerometerChange?.Invoke((Vector3)soundLevel), basePhidgetData.port, basePhidgetData.hubSerialNumber);
        }
    }
}

