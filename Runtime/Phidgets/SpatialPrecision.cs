using System;
using Nimlok.Phidgets.IndividualPhidgets;
using Phidget22;
using Phidget22.Events;

namespace Nimlok.Phidgets
{
    public class SpatialPrecision: BaseIndividualPhidget
    {
        private Spatial spatialPrecision => (Spatial)Phidget;

        private Action<double[]> onAccelerometer;

        private Action<double[]> onRotation;
        
        public override void AddListener(Action<object> onStateChanged, IndividualPhidgetType type)
        {
            if (type == IndividualPhidgetType.SpatialAccelormeter)
            {
                onAccelerometer += (d) => onStateChanged?.Invoke(d);
            } else if (type == IndividualPhidgetType.SpatialRotation)
            {
                onRotation += (r) => onStateChanged?.Invoke(r);
            }
        }

        public override void RemoveListener(Action<object> onStateChanged, IndividualPhidgetType type)
        {
            if (type == IndividualPhidgetType.SpatialAccelormeter)
            {
                onAccelerometer -= (d) => onStateChanged?.Invoke(d);
            } else if (type == IndividualPhidgetType.SpatialRotation)
            {
                onRotation -= (r) => onStateChanged?.Invoke(r);
            }
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