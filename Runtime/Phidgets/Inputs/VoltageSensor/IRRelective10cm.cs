using Phidget22;

namespace Phidgets
{
    public class IRReflective10cm: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = VoltageRatioSensorType.PN_1103;
        }
    }
}