namespace Nimlok.Phidgets
{
    public class Joystick: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = Phidget22.VoltageRatioSensorType.PN_1113;
        }
    }
}