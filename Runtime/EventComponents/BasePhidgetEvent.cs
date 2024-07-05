using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Phidgets.EventComponents
{
    [Serializable]
    public struct BasePhidgetData
    {
        [OnValueChanged("UpdateSerialNumber")]
        public Hub hub;
        public int hubSerialNumber;
        public int port;
        
        public int GetSerialNumber()
        {
            if (hub != null)
            {
                return hub.serialNumber;
            }

            return hubSerialNumber > 0 ? hubSerialNumber : -1;
        }

        private void UpdateSerialNumber()
        {
            if (hub == null)
                return;

            hubSerialNumber = hub.serialNumber;
        }
    }
    
    [Serializable]
    public abstract class BasePhidgetEvent: MonoBehaviour
    {
        public BasePhidgetData basePhidgetData;
    }

    public abstract class InputPhidgetEvent : BasePhidgetEvent
    {
        private bool initialise;

        private void OnEnable()
        {
            initialise = PhidgetControllerEvents.AddListener != null;
            AddListener();
        }

        private void Start()
        {
            if (initialise)
                return;
            AddListener();
        }

        private void OnDisable()
        {
            RemoveListener();
        }

        protected abstract void AddListener();
        protected abstract void RemoveListener();

        protected void ListenerToAdd(Action<object> onStateChange)
        {
            PhidgetControllerEvents.AddListener?.Invoke(onStateChange, basePhidgetData.port, basePhidgetData.hubSerialNumber);
        }

        protected void ListenerToRemove(Action<object> onStateChange)
        {
            PhidgetControllerEvents.RemoveListener?.Invoke(onStateChange, basePhidgetData.port, basePhidgetData.hubSerialNumber);
        }
    }
}