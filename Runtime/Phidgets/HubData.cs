using Nimlok.Phidgets.EventComponents;

namespace Nimlok.Phidgets
{
    public class HubData
    {
        public int serialNumber;
        public PhidgetInputType[] phidgetTypes = new PhidgetInputType[6];
        private BasePhidget[] phidgets = new BasePhidget[6];
        private BasePhidgetEvent[] phidgetEvents = new BasePhidgetEvent[6];
    }
}