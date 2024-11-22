using System;

namespace Nimlok.Phidgets
{
    [Serializable]
    public enum PhidgetInputType
    {
        None,
        DigitalInput,
        VoltageRatio,
        IRReflective10cm,
        IRReflective4mm,
        IRDistance,
        Temperature,
        Vibration,
        Touch,
        Distance,
        Accelerometer,
        Magnetic,
        Motion,
        Joystick,
        Sound,
        Sonar,
        MotorStepper2A,
        MotorStepper4A,
        RFIDVint
    }
}