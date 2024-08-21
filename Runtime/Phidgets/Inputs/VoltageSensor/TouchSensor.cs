namespace Nimlok.Phidgets.Voltage
{
    public class TouchSensor: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = Phidget22.VoltageRatioSensorType.PN_1129;
        }
    }
}