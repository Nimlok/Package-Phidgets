using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/SoundSensor")]
    public class PhidgetSoundSensor : PhidgetBaseObject
    {
        private SoundSensor soundSensor => (SoundSensor)Phidget;

        public Action<float> onSoundChange;

        public override void InitialisePhidget()
        {
            Phidget = new SoundSensor();
            soundSensor.SPLChange += OnSoundSensorChange;
            base.InitialisePhidget();
        }

        private void OnSoundSensorChange(object sender, SoundSensorSPLChangeEventArgs e)
        {
            var soundLevel = (float)e.DBC;
            LogState(soundLevel.ToString());
            ThreadManager.instance.AddToMainThread(() => onSoundChange?.Invoke(soundLevel));
        }
    }
}

