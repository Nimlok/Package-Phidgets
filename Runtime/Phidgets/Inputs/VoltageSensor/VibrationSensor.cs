using Phidget22;

namespace Nimlok.Phidgets.Inputs.VoltageSensor
{
    public class VibrationSensor: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = VoltageRatioSensorType.PN_1104;
        }
    }
}