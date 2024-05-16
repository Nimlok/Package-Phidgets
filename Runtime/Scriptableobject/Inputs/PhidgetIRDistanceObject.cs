using Phidget22;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/IRDistance")]
    public class PhidgetIRDistanceObject: PhidgetVoltageSensorObject
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            voltageRatioInput.SensorType = VoltageRatioSensorType.PN_1101_Sharp2D120X;
        }
    }
}