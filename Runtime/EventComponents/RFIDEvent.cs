using System;
using Nimlok.Phidgets.EventComponents;
using Nimlok.Phidgets.IndividualPhidgets;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Nimlok.Phidgets.Runtime.EventComponents
{
    public class RFIDEvent: MonoBehaviour
    {
        [OnValueChanged("UpdateSerialNumber")]
        public RFID rfid;
        
        public int SerialID;
        
        [Space]
        [SerializeField] private UnityEvent<string> onTagRead;
        [SerializeField] private UnityEvent onTagLost;

        private bool initialised;
        
        private void UpdateSerialNumber()
        {
            if (rfid == null)
                return;

            SerialID = rfid.serialID;
        }

        private void OnGUI()
        {
            UpdateSerialNumber();
        }

        private void OnEnable()
        {
            initialised = IndividualPhidgetManager.AddListener != null;
            
            if(initialised)
                AddListener();
        }

        private void Start()
        {
            if (initialised)
                return;
            
            
            AddListener();
        }

        private void OnDisable()
        {
           RemoveListeners();
        }

        private void AddListener()
        {
            if(onTagRead != null)
                IndividualPhidgetManager.AddListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDReader, SerialID == 0 ? -1: SerialID);
            if(onTagLost != null)
                IndividualPhidgetManager.AddListener?.Invoke( (o) => onTagLost?.Invoke(), IndividualPhidgetType.RFIDLost, SerialID == 0 ? -1: SerialID);
        }

        private void RemoveListeners()
        {
            if(onTagRead != null)
                IndividualPhidgetManager.RemoveListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDReader, SerialID == 0 ? -1: SerialID);
            if(onTagLost != null)
                IndividualPhidgetManager.RemoveListener?.Invoke((o) => onTagLost?.Invoke(), IndividualPhidgetType.RFIDLost, SerialID == 0 ? -1: SerialID);
        }
    }
}