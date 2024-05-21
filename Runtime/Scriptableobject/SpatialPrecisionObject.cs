using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/SpatialPrecision")]
    public class SpatialPrecisionObject: PhidgetBaseObject
    {
        private Spatial spatialPrecision => (Spatial)Phidget;

        public Action<double[]> OnAccelerometer;

        public Action<double[]> OnRotation;

        public override void InitialisePhidget()
        {
            Phidget = new Spatial();
            base.InitialisePhidget();
            spatialPrecision.SpatialData += SpatialData;
            spatialPrecision.AlgorithmData += SpatialData;
        }

        private void SpatialData(object sender, SpatialAlgorithmDataEventArgs e)
        {
            LogState($"{e.Quaternion[0]},{e.Quaternion[1]}, {e.Quaternion[2]}");
            ThreadManager.instance.AddToMainThread(() => OnRotation?.Invoke(e.Quaternion));
        }

        private void SpatialData(object sender, SpatialSpatialDataEventArgs e)
        { 
            LogState($"{e.Acceleration[0]},{e.Acceleration[1]}, {e.Acceleration[2]}");
            ThreadManager.instance.AddToMainThread(() => OnAccelerometer?.Invoke(e.Acceleration));
        }
    }
}