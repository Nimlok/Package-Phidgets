using Nimlok.Phidgets.IndividualPhidgets;
using UnityEngine;
using UnityEngine.Events;

namespace Nimlok.Phidgets.Runtime.EventComponents
{
    public class RFIDEvent: MonoBehaviour
    {
        [SerializeField] private int SerialID;
        
        [Space]
        [SerializeField] private UnityEvent<string> onTagRead;

        [SerializeField] private UnityEvent onTagLost;

        private bool initialised;
        
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
                IndividualPhidgetManager.AddListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDLost, SerialID == 0 ? -1: SerialID);
        }

        private void RemoveListeners()
        {
            if(onTagRead != null)
                IndividualPhidgetManager.RemoveListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDReader, SerialID == 0 ? -1: SerialID);
            if(onTagLost != null)
                IndividualPhidgetManager.RemoveListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDLost, SerialID == 0 ? -1: SerialID);
        }
    }
}