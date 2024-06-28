namespace Phidgets.Inputs.VoltageSensor
{
    public class IRDistanceAdapter: BaseVoltageSensor
    {
        public override void InitialisePhidget()
        {
            base.InitialisePhidget();
            GetVoltageRatioInput.SensorType = Phidget22.VoltageRatioSensorType.PN_1101_Sharp2D120X;
        }
    }
}