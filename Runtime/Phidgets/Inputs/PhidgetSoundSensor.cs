using Phidget22;
using Phidget22.Events;

namespace Phidgets
{
    public class BaseSoundSensor : BasePhidget
    {
        private SoundSensor soundSensor => (SoundSensor)Phidget;
        
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
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(soundLevel));
        }
    }
}

