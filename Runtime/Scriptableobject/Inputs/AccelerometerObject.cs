using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/Accelerometer")]
    public class Accelerometer : PhidgetBaseObject
    {
        public Action<Vector3> OnStateChange;
    
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
            ThreadManager.instance.AddToMainThread(() => OnStateChange?.Invoke(acceleration));
        }

        public override void ClosePhidget()
        {
            accelerometer.AccelerationChange -= StateChange;
            base.ClosePhidget();
        }
    }
}

