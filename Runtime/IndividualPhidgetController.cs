using System;
using System.Collections.Generic;
using System.Linq;
using Phidgets.IndividualPhidgets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Phidgets
{
    public struct IndividaulPhidgetEvent
    {
        public int serialNumber;
        public IndividualPhidgetType type;
        public Action<object> phidgetevent;
    }
    
    public class IndividualPhidgetController: MonoBehaviour
    {
        private List<BaseIndividualPhidget> individualPhidgets = new List<BaseIndividualPhidget>();
        
        public static Action<Action<object>, IndividualPhidgetType, int> AddListener;
        public static Action<Action<object>, IndividualPhidgetType, int> RemoveListener;

        private bool initialised;
        private List<IndividaulPhidgetEvent> eventsToAdd = new List<IndividaulPhidgetEvent>();
        
        #region EDITOR
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        private void AddPhidget(IndividualPhidgetType type)
        {
            if (type == IndividualPhidgetType.None)
                return;
            
            var phidgetGameObject = new GameObject();
            BaseIndividualPhidget phidget = null;
            switch (type)
            {
                case IndividualPhidgetType.RFID:
                    phidget = phidgetGameObject.AddComponent<RFID>();
                    break;
                default:
                    break;
            }

            if (phidget == null)
                return;
            
            phidgetGameObject.name = $"{type}";
            phidgetGameObject.transform.SetParent(transform);
            individualPhidgets.Add(phidget);
        }
        
        
        #endregion
        
        #region UNITY
        private void OnDisable()
        {
            AddListener -= AddEventListener;
            RemoveListener -= RemoveEventListener;
        }

        private void Awake()
        {
            AddListener += AddEventListener;
            RemoveListener += RemoveEventListener;
        }

        private void Start()
        {
            InitialiseIndividualPhidgets();
            initialised = true;
            InitialiseEvents();
        }

        private void OnApplicationQuit()
        {
            foreach (var phidget in individualPhidgets)
            {
                phidget.ClosePhidget();
            }
        }
        #endregion

        private void InitialiseIndividualPhidgets()
        {
            if (individualPhidgets.Count <= 0)
            {
                individualPhidgets = GetComponentsInChildren<BaseIndividualPhidget>().ToList();
            }
                

            foreach (var phidget in individualPhidgets)
            {
                phidget.InitialisePhidget();
            }
        }

        private void InitialiseEvents()
        {
            if (eventsToAdd.Count <= 0)
                return;

            foreach (var phidgetEvent in eventsToAdd)
            {
                AddPhidgetEvent(phidgetEvent.phidgetevent, phidgetEvent.type, phidgetEvent.serialNumber);
            }
            
            eventsToAdd.Clear();
        }
        
        private void AddEventListener(Action<object> onPhidgetEvent, IndividualPhidgetType type, int serialNumber = -1)
        {
            if (!initialised)
            {
                eventsToAdd.Add(new IndividaulPhidgetEvent(){
                    serialNumber =  serialNumber,
                    phidgetevent = onPhidgetEvent,
                    type = type
                });
            } else
            {
                AddPhidgetEvent(onPhidgetEvent, type, serialNumber);
            }
        }

        private void AddPhidgetEvent(Action<object> onPhidgetEvent, IndividualPhidgetType type, int serialNumber = -1)
        {
            var phidget = FindPhidget(type, serialNumber);

            if (phidget == null)
                return;
            
            phidget.AddListener(onPhidgetEvent);
        }
        
        private void RemoveEventListener(Action<object> onPhidgetEvent, IndividualPhidgetType type, int serialNumber = -1)
        {
            var phidget = FindPhidget(type, serialNumber);

            if (phidget == null)
                return;
            
            phidget.RemoveListener(onPhidgetEvent);
        }

        private BaseIndividualPhidget FindPhidget(IndividualPhidgetType phidgetType, int serialNumber)
        {
            return serialNumber == -1 ? individualPhidgets.Find(x => x.PhidgetType == phidgetType) 
                : individualPhidgets.Find(x => x.GetSerial == serialNumber);
        }
    }
}