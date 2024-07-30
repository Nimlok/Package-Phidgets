using System;
using Phidgets.EventComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Phidgets
{
    [Serializable]
    public class Hub: MonoBehaviour
    {
        [Space,OnValueChanged("OnSerialNumberChanged")] public int serialNumber;
        
        [Space]
        [OnValueChanged("OnPhidgetTypeChanged", true), SerializeField] 
        private PhidgetData[] phidgets = new PhidgetData[6];
        
        private BasePhidget[] basePhidgets = new BasePhidget[6];
        private BasePhidgetEvent[] phidgetEvents = new BasePhidgetEvent[6];
        private Phidget22.Hub hub;
        private bool initialised;

        public bool Initialised => initialised;

        public BasePhidget[] GetPorts => basePhidgets;
        
        #region  EDITOR
        private bool eventsAdded;
        
        [PropertySpace]
        [Button(ButtonSizes.Medium)]
        private void AddPhidgetEvents()
        {
            eventsAdded = true;
            
            for (int i = 0; i < phidgets.Length; i++)
            {
                if (phidgets[i].inputType == PhidgetInputType.None)
                    continue;
                
                AddPhidgetEventComponent(phidgets[i].inputType, phidgets[i].port);
            }
        }

        [Button(ButtonSizes.Medium)]
        private void RemovePhidgetEvents()
        {
            eventsAdded = false;
            for (int i = 0; i < phidgetEvents.Length; i++)
            {
                RemovePhidgetEvent(i);
            }
        }
        
        private void OnPhidgetTypeChanged()
        {
            for (int i = 0; i < phidgets.Length; i++)
            {
                phidgets[i].port = i;
                phidgets[i].serial = serialNumber;
                if (phidgets[i].inputType == PhidgetInputType.None)
                {
                    if (!eventsAdded)
                        continue;
                    RemovePhidgetEvent(i);
                }
                else
                {
                    if (!eventsAdded)
                        continue;
                    AddPhidgetEventComponent(phidgets[i].inputType, i);
                }
            }
        }

        private void AddPhidgetEventComponent(PhidgetInputType phidgetInputType, int index)
        {
            BasePhidgetEvent phidgetEvent = GetPhidgetEvent.GetEvent(gameObject, phidgetInputType);
            
            if (phidgetEvent == null) 
                return;

            phidgetEvent.basePhidgetData.hub = this;
            phidgetEvent.basePhidgetData.port = index;
            phidgetEvent.basePhidgetData.hubSerialNumber = serialNumber;
            phidgetEvents[index] = phidgetEvent;
        }

        private void RemovePhidgetEvent(int index)
        {
            DestroyImmediate(phidgetEvents[index]);
        }
        
        private void OnSerialNumberChanged()
        {
            RenameHub();
            //AddSerialForEvents();
        }

        #endregion

        public void AssignPorts()
        {
            for (int i = 0; i < phidgets.Length; i++)
            {
                phidgets[i].port = i;
            }
        }
        
        public void Initialise(Phidget22.Hub hub)
        {
            initialised = true;
            serialNumber = hub.DeviceSerialNumber;
            RenameHub();
            this.hub = hub;
        }

        public void InitialiseInputs()
        {
            if (hub == null)
                return;

            for (int i = 0; i < phidgets.Length; i++)
            {
                if (phidgets[i].inputType == PhidgetInputType.None)
                    continue;

                basePhidgets[i] = phidgets[i].GetPhidget();
            }
            
            foreach (var phidget in basePhidgets)
            {
                if (phidget == null)
                    continue;
                
                phidget.SetSerial = hub.DeviceSerialNumber;
                phidget.InitialisePhidget();
            }
        }
        
        public void Close()
        {
            if (basePhidgets.Length <= 0)
                return;
            
            foreach (var phidget in basePhidgets)
            {
                if (phidget == null)
                    continue;
                
                phidget.ClosePhidget();
            }
        }

        public void TriggerPhidget(object value, int phidgetNumber)
        {
            var phidget = basePhidgets[phidgetNumber];

            if (phidget == null)
                return;

            phidget.TriggerPhidget(value);
        }

        public BasePhidget FindPhidget(int port)
        {
            foreach (var phidget in basePhidgets)
            {
                if(phidget == null)
                    continue;
                
                if (phidget.port == port)
                {
                    Debug.Log($"phidget Found: {phidget.port}");
                    return phidget;
                }
            }

            return null;
        }
        
        private void RenameHub()
        {
            gameObject.name = $"Hub: {serialNumber}";
        }
    }
}