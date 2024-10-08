using Phidget22.Events;

namespace Nimlok.Phidgets
{
    public class SonarSensor : BasePhidget
    {
        private Phidget22.DistanceSensor sonarSensor => (Phidget22.DistanceSensor)Phidget;
        
        public override PhidgetInputType PhidgetInputType => PhidgetInputType.Sonar;
        
        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.DistanceSensor();
            sonarSensor.DistanceChange += OnSonarChange;
            base.InitialisePhidget();
        }

        private void OnSonarChange(object sender, DistanceSensorDistanceChangeEventArgs e)
        {
            var distance = (float)e.Distance;
            LogState(distance.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(distance));
        }
    }
}

