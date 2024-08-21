using Phidget22;

namespace Nimlok.Phidgets.Inputs.VoltageSensor
{
    public class IRReflective4mm: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = VoltageRatioSensorType.PN_1146;
        }
    }
}