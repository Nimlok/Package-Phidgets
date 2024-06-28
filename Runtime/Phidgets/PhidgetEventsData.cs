using System;

namespace Phidgets
{
    public class PhidgetEventsData
    {
        public Action<object> action;
        public int port;
        public int serialNumber;
    }
}