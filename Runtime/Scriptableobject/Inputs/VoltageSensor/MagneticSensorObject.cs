using Phidget22;
using UnityEngine;

namespace Phidgets.Voltage
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/MagneticSensor")]
    public class MagneticObject : PhidgetVoltageSensorObject
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            voltageRatioInput.SensorType = VoltageRatioSensorType.PN_1108;
        }
    }
}