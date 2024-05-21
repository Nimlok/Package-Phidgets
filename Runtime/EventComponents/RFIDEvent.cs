using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Runtime.EventComponents
{
    public class RFIDEvent: MonoBehaviour
    {
        [SerializeField] private RFIDObject sensorObject;
        
        [Space]
        [SerializeField] private UnityEvent<string> unityEvent;

        private void OnEnable()
        {
            if (sensorObject != null)
                sensorObject.OnTagRead += (tag) => unityEvent?.Invoke(tag);
        }
        
        private void OnDisable()
        {
            if (sensorObject != null)
                sensorObject.OnTagRead -= (tag) => unityEvent?.Invoke(tag);
        }
    }
}