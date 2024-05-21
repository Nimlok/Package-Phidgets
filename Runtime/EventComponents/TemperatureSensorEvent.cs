using Phidgets.Voltage;
using UnityEngine;
using UnityEngine.Events;

namespace Phidgets
{
    public class TemperatureSensorEvent: MonoBehaviour
    {
        [SerializeField] private TemperatureSensorObject sensorObject;
        
        [Space]
        [SerializeField] private UnityEvent<float> unityEvent;

        private void OnEnable()
        {
            if (sensorObject != null)
                sensorObject.OnStateChange += (voltage) => unityEvent?.Invoke(voltage);
        }
        
        private void OnDisable()
        {
            if (sensorObject != null)
                sensorObject.OnStateChange -= (voltage) => unityEvent?.Invoke(voltage);
        }
    }
}