using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/RFID")]
    public class RFIDObject : PhidgetBaseObject
    {
        private RFID rfid => (RFID)Phidget;

        public Action<string> OnTagRead;

        public override void InitialisePhidget()
        {
            Phidget = new RFID();
            base.InitialisePhidget();
            rfid.Tag += TagRead;
        }

        private void TagRead(object sender, RFIDTagEventArgs e)
        {
            var tag = e.Tag;
            LogState(tag);
            ThreadManager.instance.AddToMainThread((() => OnTagRead?.Invoke(tag)));
        }
    }
}
