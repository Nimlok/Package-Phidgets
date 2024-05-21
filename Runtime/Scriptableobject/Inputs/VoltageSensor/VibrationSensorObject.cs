using Phidget22;
using UnityEngine;

namespace Phidgets.Inputs.VoltageSensor
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/VibrationSensor")]
    public class VibrationSensorObject: PhidgetVoltageSensorObject
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            voltageRatioInput.SensorType = VoltageRatioSensorType.PN_1104;
        }
    }
}