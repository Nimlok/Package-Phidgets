
using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class VoltageSensorEvent : MonoBehaviour
    {
        [SerializeField] private PhidgetVoltageSensorObject phidgetVoltageRatio;
        
        [Space]
        [SerializeField] private UnityEvent<float> unityEvent;

        private void OnEnable()
        {
            if (phidgetVoltageRatio != null)
                phidgetVoltageRatio.voltageChanged += (voltage) => unityEvent?.Invoke(voltage);
        }
        
        private void OnDisable()
        {
            if (phidgetVoltageRatio != null)
                phidgetVoltageRatio.voltageChanged -= (voltage) => unityEvent?.Invoke(voltage);
        }
    }
}

