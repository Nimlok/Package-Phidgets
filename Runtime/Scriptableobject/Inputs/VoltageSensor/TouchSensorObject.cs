using UnityEngine;

namespace Phidgets.Voltage
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/TouchSensor")]
    public class TouchSensorObject: PhidgetVoltageSensorObject
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            voltageRatioInput.SensorType = Phidget22.VoltageRatioSensorType.PN_1129;
        }
    }
}