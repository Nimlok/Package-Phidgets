using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phidgets
{
    [RequireComponent(typeof(PhidgetHubManager))]
    public class HubEventsManager: MonoBehaviour
    {
        private PhidgetHubManager phidgetHubManager;
        
        //Needed for exception case if the phidget aren't initialised before an attempt at adding an event
        private bool initialised;
        private readonly List<PhidgetEventsData> eventsToAdd = new List<PhidgetEventsData>();
        
        public static Action<Action<object>, int, int> AddListener;
        public static Action<Action<object>, int, int> RemoveListener;
        public static Action<object, int, int> ActivatePhidget;
        
        private void OnEnable()
        {
            PhidgetHubManager.OnHubInitialised += InitialiseEvents;
            ActivatePhidget += phidgetHubManager.TriggerPhidget;
            AddListener += AddEventListener;
            RemoveListener += RemoveEventListener;
        }

        private void OnDisable()
        {
            PhidgetHubManager.OnHubInitialised -= InitialiseEvents;
            ActivatePhidget -= phidgetHubManager.TriggerPhidget;
            AddListener -= AddEventListener;
            RemoveListener -= RemoveEventListener;
        }
        
        private void Awake()
        {
            phidgetHubManager = GetComponent<PhidgetHubManager>();
            
        }

        private void InitialiseEvents()
        {
            if (eventsToAdd.Count <= 0)
                return;
            
            foreach (var phidgetEvent in eventsToAdd)
            {
                AddPhidgetEvent(phidgetEvent.action, phidgetEvent.port, phidgetEvent.serialNumber);
            }
            
            eventsToAdd.Clear();
        }
        
        private void AddEventListener(Action<object> onPhidgetEvent, int port = -1, int serialNumber = -1)
        {
            if (!initialised)
            {
                eventsToAdd.Add(new PhidgetEventsData()
                {
                    port = port,
                    serialNumber =  serialNumber,
                    action = onPhidgetEvent
                });
            } else
            {
                AddPhidgetEvent(onPhidgetEvent, port, serialNumber);
            }
        }

        private void AddPhidgetEvent(Action<object> onPhidgetEvent, int port = -1, int serialNumber = -1)
        {
            var phidget = phidgetHubManager.FindBasePhidget(port, serialNumber);
            if (phidget != null) 
                phidget.onStateChange += (o) => onPhidgetEvent?.Invoke(o);
        }
        
        private void RemoveEventListener(Action<object> onPhidgetEvent, int port = -1, int serialNumber = -1)
        {
            var phidget = phidgetHubManager.FindBasePhidget(port, serialNumber);
            if (phidget == null)
                return;

            phidget.onStateChange -= (o) => onPhidgetEvent?.Invoke(o);
        }
    }
}