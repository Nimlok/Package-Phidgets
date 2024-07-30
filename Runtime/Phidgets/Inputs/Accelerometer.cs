using System;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    public class Accelerometer : BasePhidget
    {
        public override PhidgetInputType PhidgetInputType => PhidgetInputType.Accelerometer;
        
        private Phidget22.Accelerometer accelerometer => (Phidget22.Accelerometer)Phidget;
        
        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.Accelerometer();
            accelerometer.AccelerationChange += StateChange;
            base.InitialisePhidget();
        }

        private void StateChange(object sender, AccelerometerAccelerationChangeEventArgs e)
        {
            var acceleration = new Vector3((float)e.Acceleration[0], (float)e.Acceleration[1], (float)e.Acceleration[2]);
            LogState(acceleration.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(acceleration));
        }

        public override void ClosePhidget()
        {
            accelerometer.AccelerationChange -= StateChange;
            base.ClosePhidget();
        }
    }
}

