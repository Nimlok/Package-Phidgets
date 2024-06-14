using Phidget22.Events;
//using Sirenix.OdinInspector;
using UnityEngine;

namespace Phidgets
{
    //[InlineEditor]
    public abstract class PhidgetBaseObject: ScriptableObject
    {
        [SerializeField] protected int port;
        [SerializeField] protected bool debug;
        
        private int serialID;

        protected Phidget22.Phidget Phidget;
        
        public int SetSerial
        {
            set => serialID = value;
        }

        public virtual void InitialisePhidget()
        {
            if (Phidget == null)
                return;
            
            Phidget.DeviceSerialNumber = serialID;
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

        //DS 14/0/624 For testing in editor
        public virtual void TriggerPhidget()
        {
            Debug.Log($"<color=lightblue>Phidget: {serialID}: {port}</color> <color=orange>Triggered</color>");
        }
        
        protected virtual void OnAttachHandler(object sender, AttachEventArgs e)
        {
            if(debug)
                Debug.Log($"<color=green>Phidget:</color> attached {sender}");
        }

        protected void LogState(string state)
        {
            if(debug)
                Debug.Log($"<color=lightblue>Phidget: {serialID}: {port}</color> <color=orange>{state}</color>");
        }
    }
}