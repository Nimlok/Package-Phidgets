namespace Nimlok.Phidgets.Inputs
{
    public class VoltageTemperatureSensor: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = Phidget22.VoltageRatioSensorType.PN_1124;
        }
    }
}