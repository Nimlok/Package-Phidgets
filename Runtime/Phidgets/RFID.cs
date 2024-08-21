using System;
using Nimlok.Phidgets.IndividualPhidgets;
using Phidget22.Events;

namespace Nimlok.Phidgets
{
    public class RFID : BaseIndividualPhidget
    {
        private Phidget22.RFID GetRfid => (Phidget22.RFID)Phidget;

        public override IndividualPhidgetType PhidgetType => IndividualPhidgetType.RFID;

        private Action<string> onTagRead;

        public Action OnTagLost;

        public override void AddListener(Action<object> onStateChanged, IndividualPhidgetType type)
        {
            if (type == IndividualPhidgetType.RFIDReader || type == IndividualPhidgetType.RFID)
            {
                onTagRead += (s) => onStateChanged?.Invoke(s);
                return;
            }

            OnTagLost += () => onStateChanged?.Invoke(null);
        }

        public override void RemoveListener(Action<object> onStateChanged, IndividualPhidgetType type)
        {
            if (type == IndividualPhidgetType.RFIDReader || type == IndividualPhidgetType.RFID)
            {
                onTagRead -= (s) => onStateChanged?.Invoke(s);
                return;
            }

            OnTagLost -= () => onStateChanged?.Invoke(null);
        }

        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.RFID();
            base.InitialisePhidget();
            GetRfid.Tag += TagRead;
            GetRfid.TagLost += TagLost;
        }

        public override void ClosePhidget()
        {
            GetRfid.Tag -= TagRead;
            GetRfid.TagLost -= TagLost;
            base.ClosePhidget();
        }
        
        private void TagLost(object sender, RFIDTagLostEventArgs e) 
        {
            LogState("Tag Lost");
            ThreadManager.instance.AddToMainThread(() => OnTagLost?.Invoke());
        }

        private void TagRead(object sender, RFIDTagEventArgs e)
        {
            LogState($"Tag Read: {e.Tag}");
            ThreadManager.instance.AddToMainThread(() => onTagRead?.Invoke(e.Tag));
        }
    }
}
