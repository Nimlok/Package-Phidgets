using System;
using Phidget22.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nimlok.Phidgets
{
    [Serializable]
    public abstract class BasePhidget
    {
        public bool debug;
        
        private int serialID;
        [ReadOnly]public int port;
        private bool initialised;
        
        protected Phidget22.Phidget Phidget;
        
        public virtual PhidgetInputType PhidgetInputType => PhidgetInputType.None;

        public virtual Phidget22.Phidget GetBasePhidget => Phidget;

        public Action<object> onStateChange;

        public bool GetInitialised => initialised;
        
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
                initialised = true;
            }
            catch (Exception e)
            {
                initialised = false;
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