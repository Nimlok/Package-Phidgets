using Phidgets;
using UnityEngine;
using UnityEngine.Events;

public class PhidgetDigitalInputEvent : MonoBehaviour
{
    [SerializeField] private PhidgetDigitalInput phidgetDigitalInput;
    [SerializeField] private UnityEvent<bool> onDigitalStateChange;

    private void OnEnable()
    {
        if (phidgetDigitalInput != null)
            phidgetDigitalInput.OnStateChange += (state) => onDigitalStateChange?.Invoke(state);
    }

    private void OnDisable()
    {
        if (phidgetDigitalInput != null)
            phidgetDigitalInput.OnStateChange -= (state) => onDigitalStateChange?.Invoke(state);
    }
}
