using Phidgets.EventComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class AccelerometerEvent : InputPhidgetEvent
    {
        [SerializeField] private UnityEvent<Vector3> OnAccelerometerChange;

        protected override void AddListener()
        {
            ListenerToAdd(o => OnAccelerometerChange?.Invoke((Vector3)o));
        }

        protected override void RemoveListener()
        {
            ListenerToRemove(o => OnAccelerometerChange?.Invoke((Vector3)o));
        }
    }
}

