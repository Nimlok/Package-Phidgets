using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Runtime.EventComponents
{
    
    public class PhidgetSoundSensorEvent: MonoBehaviour
    {
        [SerializeField] private PhidgetSoundSensor phidgetSoundSensor;
        public UnityEvent<float> OnSoundChange;

        private void OnEnable()
        {
            if(phidgetSoundSensor != null)
                phidgetSoundSensor.onSoundChange += (soundLevel) => OnSoundChange?.Invoke(soundLevel);
        }
        
        private void OnDisable()
        {
            if(phidgetSoundSensor != null)
                phidgetSoundSensor.onSoundChange -= (soundLevel) => OnSoundChange?.Invoke(soundLevel);
        }
    }
}