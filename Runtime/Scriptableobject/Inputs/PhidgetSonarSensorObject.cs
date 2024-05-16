using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/Sonar")]
    public class PhidgetSonarSensorObject : PhidgetBaseObject
    {
        private DistanceSensor sonarSensor => (DistanceSensor)Phidget;

        public Action<float> onSonarChange;

        public override void InitialisePhidget()
        {
            Phidget = new DistanceSensor();
            sonarSensor.DistanceChange += OnSonarChange;
            base.InitialisePhidget();
        }

        private void OnSonarChange(object sender, DistanceSensorDistanceChangeEventArgs e)
        {
            var distance = (float)e.Distance;
            LogState(distance.ToString());
            ThreadManager.instance.AddToMainThread(() => onSonarChange?.Invoke(distance));
        }
    }
}

