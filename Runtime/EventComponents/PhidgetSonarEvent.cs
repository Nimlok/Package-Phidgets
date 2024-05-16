using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Runtime.EventComponents
{
    public class PhidgetSonarEvent: MonoBehaviour
    {
        [SerializeField] private PhidgetSonarSensorObject phidgetDigitalInput;
        [SerializeField] private UnityEvent<float> OnSonarChange;

        private void OnEnable()
        {
            if (phidgetDigitalInput != null)
                phidgetDigitalInput.onSonarChange += (state) => OnSonarChange?.Invoke(state);
        }

        private void OnDisable()
        {
            if (phidgetDigitalInput != null)
                phidgetDigitalInput.onSonarChange -= (state) => OnSonarChange?.Invoke(state);
        }
    }
}