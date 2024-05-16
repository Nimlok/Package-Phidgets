using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    public abstract class PhidgetBaseObject: ScriptableObject
    {
        [SerializeField] protected int port;
        [SerializeField] protected bool debug;
        
        protected int SerialID;

        protected Phidget22.Phidget Phidget;
        
        public int SetSerial
        {
            set => SerialID = value;
        }

        public virtual void InitialisePhidget()
        {
            if (Phidget == null)
                return;
            
            Phidget.DeviceSerialNumber = SerialID;
            Phidget.HubPort = port;
            Phidget.Attach += OnAttachHandler;
            Phidget.Open(500);
        }

        public virtual void ClosePhidget()
        {
            if (Phidget == null)
                return;
            
            Phidget.Close();
            Phidget = null;
        }
        
        protected virtual void OnAttachHandler(object sender, AttachEventArgs e)
        {
            if(debug)
                Debug.Log($"<color=green>Phidget:</color> attached {sender}");
        }

        protected void LogState(string state)
        {
            if(debug)
                Debug.Log($"<color=lightblue>Phidget: {SerialID}: {port}</color> {state}");
        }
    }
}