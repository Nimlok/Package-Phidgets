using Phidget22;

namespace Nimlok.Phidgets.Voltage
{
    public class MagneticSensor : BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = VoltageRatioSensorType.PN_1108;
        }
    }
}