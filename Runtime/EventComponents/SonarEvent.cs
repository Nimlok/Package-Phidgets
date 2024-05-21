using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class SonarEvent: MonoBehaviour
    {
        [SerializeField] private SonarSensorObject digitalInput;
        [SerializeField] private UnityEvent<float> OnSonarChange;

        private void OnEnable()
        {
            if (digitalInput != null)
                digitalInput.onSonarChange += (state) => OnSonarChange?.Invoke(state);
        }

        private void OnDisable()
        {
            if (digitalInput != null)
                digitalInput.onSonarChange -= (state) => OnSonarChange?.Invoke(state);
        }
    }
}