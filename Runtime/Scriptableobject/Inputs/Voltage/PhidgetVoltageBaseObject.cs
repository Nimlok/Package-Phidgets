using System;
using Phidget22;
using Phidget22.Events;

namespace Phidgets.Voltage
{
    public class PhidgetVoltageBaseObject: PhidgetBaseObject
    {
        protected VoltageRatioInput voltageRatioInput => (VoltageRatioInput)Phidget;
        
        public Action<float> voltageChanged;
        
        public override void InitialisePhidget()
        {
            Phidget = new VoltageRatioInput();
            Phidget.IsHubPortDevice = true;
            voltageRatioInput.VoltageRatioChange += VoltageRatioChange;
            voltageRatioInput.SensorChange += SensorChange;
            base.InitialisePhidget();
        }

        public override void ClosePhidget()
        {
            voltageRatioInput.SensorChange -= SensorChange;
            voltageRatioInput.VoltageRatioChange -= VoltageRatioChange;
            base.ClosePhidget();
        }

        private void VoltageRatioChange(object sender, VoltageRatioInputVoltageRatioChangeEventArgs args)
        {
            var voltageRatio = (float)args.VoltageRatio;
            LogState(voltageRatio.ToString());
            ThreadManager.instance.AddToMainThread(() => voltageChanged?.Invoke(voltageRatio));
        }

        private void SensorChange(object sender, VoltageRatioInputSensorChangeEventArgs args)
        {
            float sensorValue = (float)args.SensorValue;
            LogState($"{sensorValue} {args.SensorUnit}");
            ThreadManager.instance.AddToMainThread(() => voltageChanged?.Invoke(sensorValue));
        }
    }
}