using Phidgets;
using UnityEngine;
using UnityEngine.Events;

public class PhidgetAccelerometerEvent : MonoBehaviour
{
    [SerializeField] private PhidgetAccelerometer accelerometer;
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
