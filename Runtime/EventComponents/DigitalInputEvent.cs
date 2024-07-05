using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.EventComponents
{
    public class DigitalInputEvent : InputPhidgetEvent
    {
        [SerializeField] private UnityEvent<bool> onDigitalStateChange;
        
        protected override void AddListener()
        {
            ListenerToAdd(o => onDigitalStateChange?.Invoke((bool)o));
        }

        protected override void RemoveListener()
        {
           ListenerToRemove(o => onDigitalStateChange?.Invoke((bool)o));
        }
    }
}

