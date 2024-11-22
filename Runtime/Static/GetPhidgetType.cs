using Nimlok.Phidgets.Inputs;
using Nimlok.Phidgets.Inputs.VoltageSensor;
using Nimlok.Phidgets.Voltage;

namespace Nimlok.Phidgets
{
    public static class GetPhidgetType
    {
        public static BasePhidget GetType(PhidgetInputType type)
        {
            switch (type)
            {
                case PhidgetInputType.None:
                    return null;
                case PhidgetInputType.DigitalInput:
                    return new DigitalInput();
                case PhidgetInputType.VoltageRatio:
                    return new BaseVoltageInput();
                case PhidgetInputType.IRDistance:
                    return new IRDistanceAdapter();
                case PhidgetInputType.IRReflective4mm:
                    return new IRReflective4mm();
                case PhidgetInputType.IRReflective10cm:
                    return new IRReflective10cm();
                case PhidgetInputType.Temperature:
                    return new VoltageTemperatureSensor();
                case PhidgetInputType.Vibration:
                    return new VibrationSensor();
                case PhidgetInputType.Distance:
                    return new DistanceSensor();
                case PhidgetInputType.Touch:
                    return new TouchSensor();
                case PhidgetInputType.Accelerometer:
                    return new Accelerometer();
                case PhidgetInputType.Magnetic:
                    return new MagneticSensor();
                case PhidgetInputType.Motion:
                    return new MotionSensor();
                case PhidgetInputType.Joystick:
                    return new Joystick();
                case PhidgetInputType.Sound:
                    return new BaseSoundSensor();
                case PhidgetInputType.Sonar:
                    return new SonarSensor();
                case PhidgetInputType.MotorStepper4A:
                    return new StepMotor();
                case PhidgetInputType.MotorStepper2A:
                    return new StepMotor();
                 case PhidgetInputType.RFIDVint:
                     return new RFIDVint();
            }

            return null;
        }
    }
}