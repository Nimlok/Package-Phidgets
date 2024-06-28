using System;
using Phidget22.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Phidgets
{
    [Serializable]
    public abstract class BasePhidget
    {
        public bool debug;
        
        private int serialID;
        [ReadOnly]public int port;

        protected Phidget22.Phidget Phidget;
        
        public virtual PhidgetInputType PhidgetInputType => PhidgetInputType.None;

        public Action<object> onStateChange;
        
        public int SetSerial
        {
            set => serialID = value;
        }

        public virtual void InitialisePhidget()
        {
            if (Phidget == null)
                return;
            try
            {
                Phidget.DeviceSerialNumber = serialID;
                Phidget.HubPort = port;
                Phidget.Attach += OnAttachHandler;
                Phidget.Open(500);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"<color=lightblue>Phidget: {serialID}: {port}</color>: {e}");
            }
        }

        public virtual void ClosePhidget()
        {
            if (Phidget == null)
                return;
            
            Phidget.Close();
            Phidget = null;
        }
        
        public virtual void TriggerPhidget(object value = null)
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