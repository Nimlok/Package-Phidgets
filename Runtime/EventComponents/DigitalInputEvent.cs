using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Events
{
    public class DigitalInputEvent : MonoBehaviour
    {
        [SerializeField] private DigitalInput digitalInput;
        [SerializeField] private UnityEvent<bool> onDigitalStateChange;

        private void OnEnable()
        {
            if (digitalInput != null)
                digitalInput.OnStateChange += (state) => onDigitalStateChange?.Invoke(state);
        }

        private void OnDisable()
        {
            if (digitalInput != null)
                digitalInput.OnStateChange -= (state) => onDigitalStateChange?.Invoke(state);
        }
    }
}

