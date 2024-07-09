using Phidgets.IndividualPhidgets;
using UnityEngine;
using UnityEngine.Events;

namespace Phidgets.Runtime.EventComponents
{
    public class RFIDEvent: MonoBehaviour
    {
        [SerializeField] private int SerialID;
        
        [Space]
        [SerializeField] private UnityEvent<string> onTagRead;

        [SerializeField] private UnityEvent onTagLost;

        private bool initialise;
        
        private void OnEnable()
        {
            initialise = IndividualPhidgetManager.AddListener != null;
            IndividualPhidgetManager.AddListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFID, SerialID == 0 ? -1: SerialID);
        }

        private void Start()
        {
            if (initialise)
                return;
            
            IndividualPhidgetManager.AddListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFID, SerialID == 0 ? -1: SerialID);
        }

        private void OnDisable()
        {
            IndividualPhidgetManager.RemoveListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFID,SerialID == 0 ? -1: SerialID);
        }
    }
}