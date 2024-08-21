using Nimlok.Phidgets.EventComponents;
using Nimlok.Phidgets.Events;
using UnityEngine;

namespace Nimlok.Phidgets
{
    public static class GetPhidgetEvent 
    {
        public static BasePhidgetEvent GetEvent(GameObject gameObject, PhidgetInputType type)
        {
            switch (type)
            {
                case PhidgetInputType.None:
                    return null;
                case PhidgetInputType.DigitalInput:
                    return gameObject.AddComponent<DigitalInputEvent>();
                case PhidgetInputType.VoltageRatio:
                    return gameObject.AddComponent<VoltageRatioEvent>();
                case PhidgetInputType.IRReflective10cm:
                case PhidgetInputType.IRReflective4mm:
                case PhidgetInputType.IRDistance:
                case PhidgetInputType.Temperature:
                case PhidgetInputType.Vibration:
                case PhidgetInputType.Distance:
                case PhidgetInputType.Magnetic:
                case PhidgetInputType.Sound:
                case PhidgetInputType.Sonar:
                case PhidgetInputType.Motion:
                    return gameObject.AddComponent<VoltageRatioEvent>();
                case PhidgetInputType.Touch:
                    return gameObject.AddComponent<DigitalInputEvent>();
                case PhidgetInputType.Accelerometer:
                    return gameObject.AddComponent<AccelerometerEvent>();
                case PhidgetInputType.MotorStepper2A:
                case PhidgetInputType.MotorStepper4A:
                    return gameObject.AddComponent<StepMotorEvents>();
                case PhidgetInputType.Joystick:
                    break;
                default:
                    return null;
            }

            return null;
        }
    }
}

