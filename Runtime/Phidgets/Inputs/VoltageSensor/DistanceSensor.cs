using Phidget22.Events;

namespace Phidgets
{
    public class DistanceSensor: BasePhidget
    {
        private Phidget22.DistanceSensor PhidgetDigitalInput => (Phidget22.DistanceSensor)Phidget;

        public override PhidgetInputType PhidgetInputType => PhidgetInputType.Distance;
        
        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.DistanceSensor();
            PhidgetDigitalInput.DistanceChange += OnDistanceChange;
            base.InitialisePhidget();
        }

        private void OnDistanceChange(object sender, DistanceSensorDistanceChangeEventArgs e)
        {
            LogState(e.Distance.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(e.Distance));
        }
    }
}