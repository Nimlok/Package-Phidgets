using System;

namespace Nimlok.Phidgets
{
    public class PhidgetEventsData
    {
        public Action<object> action;
        public int port;
        public int serialNumber;
    }
}