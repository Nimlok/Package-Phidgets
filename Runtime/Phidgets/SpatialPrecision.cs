using System;
using Phidget22;
using Phidget22.Events;

namespace Phidgets
{
    public class SpatialPrecision: BasePhidget
    {
        private Spatial spatialPrecision => (Spatial)Phidget;

        private Action<double[]> onAccelerometer;

        private Action<double[]> onRotation;

        public void AddListener(Action<object> onStateChanged)
        {
            onAccelerometer += (d) => onStateChanged?.Invoke(d);
            onRotation += (r) => onStateChanged?.Invoke(r);
        }

        public void RemoveListener(Action<object> onStateChanged)
        {
            onAccelerometer -= (d) => onStateChanged?.Invoke(d);
            onRotation -= (r) => onStateChanged?.Invoke(r);
        }

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
            ThreadManager.instance.AddToMainThread(() => onRotation?.Invoke(e.Quaternion));
        }

        private void SpatialData(object sender, SpatialSpatialDataEventArgs e)
        { 
            LogState($"{e.Acceleration[0]},{e.Acceleration[1]}, {e.Acceleration[2]}");
            ThreadManager.instance.AddToMainThread(() => onAccelerometer?.Invoke(e.Acceleration));
        }
    }
}