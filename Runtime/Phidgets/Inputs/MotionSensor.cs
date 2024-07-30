using Phidget22;
using Phidget22.Events;

namespace Phidgets.Voltage
{
    public class MotionSensor: BasePhidget
    {
        private VoltageInput motionSensor => (VoltageInput)Phidget;
        
        public override PhidgetInputType PhidgetInputType => PhidgetInputType.Motion;
        
        public override void InitialisePhidget()
        {
            Phidget = new VoltageInput();
            Phidget.IsHubPortDevice = true;
            motionSensor.SensorChange += StateChange;
            base.InitialisePhidget();
            motionSensor.SensorType = VoltageSensorType.PN_MOT2002_LOW;
        }

        
        private void StateChange(object sender,VoltageInputSensorChangeEventArgs e)
        {
            var motion = e.SensorValue;
            LogState(motion.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke((float)motion));
        }

        public override void ClosePhidget()
        {
            motionSensor.SensorChange -= StateChange;
            base.ClosePhidget();
        }
    }
}