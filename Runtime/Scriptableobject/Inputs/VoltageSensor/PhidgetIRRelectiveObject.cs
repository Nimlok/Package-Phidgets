using Phidget22;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/IRReflective")]
    public class PhidgetIRRelective: PhidgetVoltageSensorObject
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            voltageRatioInput.SensorType = VoltageRatioSensorType.PN_1103;
        }
    }
}