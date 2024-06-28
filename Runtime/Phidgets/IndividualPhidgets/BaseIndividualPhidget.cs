using System;

using Phidget22.Events;
using UnityEngine;

namespace Phidgets.IndividualPhidgets
{
    public class BaseIndividualPhidget: MonoBehaviour
    {
        [Space]
        public bool debug;
        
        public int serialID;

        protected Phidget22.Phidget Phidget;
        
        public virtual IndividualPhidgetType PhidgetType => IndividualPhidgetType.None;
        
        public int GetSerial => serialID ;
        
        public virtual void AddListener(Action<object> onStateChanged)
        {
           
        }

        public virtual void RemoveListener(Action<object> onStateChanged)
        {
           
        }

        public virtual void InitialisePhidget()
        {
            if (Phidget == null)
                return;

            try
            {
                if (serialID > 0)
                {
                    Phidget.DeviceSerialNumber = serialID;
                }
                
                Phidget.Attach += OnAttachHandler;
                Phidget.Open(500);
            }
            catch (Exception e)
            {
                LogState(e.Message);
            }
        }

        public virtual void ClosePhidget()
        {
            if (Phidget == null)
                return;
            
            Phidget.Close();
            Phidget = null;
        }
        
        public virtual void TriggerPhidget()
        {
            Debug.Log($"<color=lightblue>Phidget: {serialID}</color> <color=orange>Triggered</color>");
        }
        
        protected virtual void OnAttachHandler(object sender, AttachEventArgs e)
        {
            if(debug)
                Debug.Log($"<color=green>Phidget:</color> attached {sender}");
        }

        protected void LogState(string state)
        {
            if(debug)
                Debug.Log($"<color=lightblue>Phidget: {serialID} </color> <color=orange>{state}</color>");
        }
    }
}