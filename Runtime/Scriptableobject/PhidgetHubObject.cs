using System;
using Phidget22;
using Phidget22.Events;
//using Sirenix.OdinInspector;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Hub")]
    public class PhidgetHubObject : ScriptableObject
    {
        //[RequiredListLength(null, 6)]
        public PhidgetBaseObject[] phidgets = new PhidgetBaseObject[6];
        
        private Hub hub;

        public void Initialise()
        {
            InitialiseHub();
            InitialiseInputs();
        }

        public void Close()
        {
            if (phidgets.Length <= 0)
                return;
            
            foreach (var phidget in phidgets)
            {
                if (phidget == null)
                    continue;
                
                phidget.ClosePhidget();
            }
        }
        
        private void InitialiseHub()
        {
            try
            {
                hub = new Hub();
                hub.Attach += OnAttachHandler;
                hub.Open(500);
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to add hub: {e}");
            }
        }

        private void InitialiseInputs()
        {
            if (phidgets.Length <= 0)
                return;
            
            foreach (var phidget in phidgets)
            {
                if (phidget == null)
                    continue;
                
                phidget.SetSerial = hub.DeviceSerialNumber;
                phidget.InitialisePhidget();
            }
        }
        
        void OnAttachHandler(object sender, AttachEventArgs e)
        {
            Debug.Log($"<color=green>Hub:</color> attached {sender}");
        }
    }
}
