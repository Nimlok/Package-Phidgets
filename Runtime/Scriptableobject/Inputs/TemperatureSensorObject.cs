using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets.Voltage
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/TemperatureSensor")]
    public class TemperatureSensorObject: PhidgetBaseObject
    {
        public Action<float> OnStateChange;
    
        private TemperatureSensor TemperatureSensor => (TemperatureSensor)Phidget;
    
        public override void InitialisePhidget()
        {
            Phidget = new TemperatureSensor();
            TemperatureSensor.TemperatureChange += StateChange;
            base.InitialisePhidget();
        }

        private void StateChange(object sender, TemperatureSensorTemperatureChangeEventArgs e)
        {
            var temperature = e.Temperature;
            LogState(temperature.ToString());
            ThreadManager.instance.AddToMainThread(() => OnStateChange?.Invoke((float)temperature));
        }

        
        public override void ClosePhidget()
        {
            TemperatureSensor.TemperatureChange -= StateChange;
            base.ClosePhidget();
        }
    }
}