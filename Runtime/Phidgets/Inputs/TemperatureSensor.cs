using Phidget22.Events;

namespace Nimlok.Phidgets.Voltage
{
    public class TemperatureSensor: BasePhidget
    {
        private Phidget22.TemperatureSensor GetTemperatureSensor => (Phidget22.TemperatureSensor)Phidget;
        
        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.TemperatureSensor();
            GetTemperatureSensor.TemperatureChange += StateChange;
            GetTemperatureSensor.IsHubPortDevice = true;
            base.InitialisePhidget();
        }

        private void StateChange(object sender, TemperatureSensorTemperatureChangeEventArgs e)
        {
            var temperature = e.Temperature;
            LogState(temperature.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke((float)temperature));
        }

        
        public override void ClosePhidget()
        {
            GetTemperatureSensor.TemperatureChange -= StateChange;
            base.ClosePhidget();
        }
    }
}