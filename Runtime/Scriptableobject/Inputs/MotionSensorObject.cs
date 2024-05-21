using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets.Voltage
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/MotionSensor")]
    public class MotionSensorObject: PhidgetBaseObject
    {
        public Action<float> OnStateChange;
    
        private VoltageInput motionSensor => (VoltageInput)Phidget;
    
        public override void InitialisePhidget()
        {
            Phidget = new VoltageInput();
            motionSensor.SensorChange += StateChange;
            base.InitialisePhidget();
            motionSensor.SensorType = VoltageSensorType.PN_MOT2002_LOW;
        }

        
        private void StateChange(object sender,VoltageInputSensorChangeEventArgs e)
        {
            var motion = e.SensorValue;
            LogState(motion.ToString());
            ThreadManager.instance.AddToMainThread(() => OnStateChange?.Invoke((float)motion));
        }

        public override void ClosePhidget()
        {
            motionSensor.SensorChange -= StateChange;
            base.ClosePhidget();
        }
    }
}