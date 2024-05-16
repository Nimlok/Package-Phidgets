using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/Thumbstick")]
    public class PhidgetVoltageSensorObject: PhidgetBaseObject
    {
        protected VoltageRatioInput voltageRatioInput => (VoltageRatioInput)Phidget;
        
        public Action<float> voltageChanged;
        
        public override void InitialisePhidget()
        {
            Phidget = new VoltageRatioInput();
            Phidget.IsHubPortDevice = true;
            voltageRatioInput.SensorChange += VoltageRatioChange;
            base.InitialisePhidget();
            voltageRatioInput.SensorType = VoltageRatioSensorType.PN_1113;
        }

        private void VoltageRatioChange(object sender, VoltageRatioInputSensorChangeEventArgs e)
        {
            float sensorValue = (float)e.SensorValue;
            LogState($"{sensorValue} {e.SensorUnit}");
            ThreadManager.instance.AddToMainThread(() => voltageChanged?.Invoke(sensorValue));
        }

        public override void ClosePhidget()
        {
            voltageRatioInput.SensorChange -= VoltageRatioChange;
            base.ClosePhidget();
        }
    }
}