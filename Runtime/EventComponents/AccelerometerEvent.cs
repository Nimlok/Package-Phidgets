using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class AccelerometerEvent : MonoBehaviour
    {
        [SerializeField] private Accelerometer accelerometer;
        [SerializeField] private UnityEvent<Vector3> OnAccelerometerChange;

        private void OnEnable()
        {
            if (accelerometer != null)
                accelerometer.OnStateChange += (vector) => OnAccelerometerChange?.Invoke(vector);
        }

        private void OnDisable()
        {
            if (accelerometer != null)
                accelerometer.OnStateChange -= (vector) => OnAccelerometerChange?.Invoke(vector);
        }
    }
}

