using System;
using System.Collections.Generic;
using System.Linq;
using EventComponents;
using Nimlok.Phidgets.IndividualPhidgets;
using Nimlok.Phidgets.Runtime.EventComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Nimlok.Phidgets
{
    public struct IndividaulPhidgetEvent
    {
        public int serialNumber;
        public IndividualPhidgetType type;
        public Action<object> phidgetevent;
    }
    
    public class IndividualPhidgetManager: MonoBehaviour
    {
        private List<BaseIndividualPhidget> individualPhidgets = new List<BaseIndividualPhidget>();
        
        public static Action<Action<object>, IndividualPhidgetType, int> AddListener;
        public static Action<Action<object>, IndividualPhidgetType, int> RemoveListener;

        private bool initialised;
        private List<IndividaulPhidgetEvent> eventsToAdd = new List<IndividaulPhidgetEvent>();
        
        #region EDITOR
        [Button(ButtonSizes.Medium, ButtonStyle.Box, Expanded = true)]
        private void AddPhidget(IndividualPhidgetType type, int SerialNumber)
        {
            if (type == IndividualPhidgetType.None)
                return;
            
            var phidgetGameObject = new GameObject();
            BaseIndividualPhidget phidget = null;
            switch (type)
            {
                case IndividualPhidgetType.RFID:
                case IndividualPhidgetType.RFIDLost:
                case IndividualPhidgetType.RFIDReader:
                    phidget = phidgetGameObject.AddComponent<RFID>();
                    phidget.name = "RFID";
                    var rfidEvent = phidgetGameObject.AddComponent<RFIDEvent>();
                    rfidEvent.rfid = phidget.GetComponent<RFID>();
                        
                    if (SerialNumber != 0)
                    {
                        phidget.name += $"{SerialNumber}";
                        phidget.serialID = SerialNumber;
                        rfidEvent.SerialID = SerialNumber;
                    }
                    
                    break;
                case IndividualPhidgetType.SpatialAccelormeter:
                    case IndividualPhidgetType.SpatialRotation:
                        phidget = phidgetGameObject.AddComponent<SpatialPrecision>();
                        phidget.name = "RFID";
                        var spatialEvent = phidgetGameObject.AddComponent<SpatialEvent>();
                        spatialEvent.spatialPrecision = phidget.GetComponent<SpatialPrecision>();
                        
                        if (SerialNumber != 0)
                        {
                            phidget.name += $"{SerialNumber}";
                            phidget.serialID = SerialNumber;
                            spatialEvent.SerialID = SerialNumber;
                        }

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
            
            phidget.AddListener(onPhidgetEvent, type);
        }
        
        private void RemoveEventListener(Action<object> onPhidgetEvent, IndividualPhidgetType type, int serialNumber = -1)
        {
            var checktype = CheckRFIDEvent(type);
            var phidget = FindPhidget(checktype, serialNumber);

            if (phidget == null)
                return;
            
            phidget.RemoveListener(onPhidgetEvent, type);
        }

        private IndividualPhidgetType CheckRFIDEvent(IndividualPhidgetType type)
        {
            return type == IndividualPhidgetType.RFIDLost || type == IndividualPhidgetType.RFIDReader
                ? IndividualPhidgetType.RFID
                : type;
        }

        private BaseIndividualPhidget FindPhidget(IndividualPhidgetType phidgetType, int serialNumber)
        {
            return serialNumber == -1 ? individualPhidgets.Find(x => x.PhidgetType == phidgetType) 
                : individualPhidgets.Find(x => x.GetSerial == serialNumber);
        }
    }
}