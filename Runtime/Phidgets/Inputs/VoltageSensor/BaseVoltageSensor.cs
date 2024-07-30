using Phidget22;
using Phidget22.Events;

namespace Phidgets
{
    public class BaseVoltageSensor: BasePhidget
    {
        public override PhidgetInputType PhidgetInputType => PhidgetInputType.VoltageRatio;
        
        protected VoltageRatioInput GetVoltageRatioInput => (VoltageRatioInput)Phidget;
        
        public override void InitialisePhidget()
        {
            Phidget = new VoltageRatioInput();
            Phidget.IsHubPortDevice = true;
            GetVoltageRatioInput.SensorChange += GetVoltageRatioChange;
            base.InitialisePhidget();
        }

        private void GetVoltageRatioChange(object sender, VoltageRatioInputSensorChangeEventArgs e)
        {
            float sensorValue = (float)e.SensorValue;
            LogState(sensorValue.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(sensorValue));
        }

        public override void ClosePhidget()
        {
            GetVoltageRatioInput.SensorChange -= GetVoltageRatioChange;
            base.ClosePhidget();
        }
    }
}