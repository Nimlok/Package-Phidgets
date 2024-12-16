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
        
        public int serialID;
        
        [Space]
        [SerializeField] private UnityEvent<string> onTagRead;
        [SerializeField] private UnityEvent onTagLost;

        private bool initialised;
        
        private void UpdateSerialNumber()
        {
            if (rfid == null)
                return;

            serialID = rfid.serialID;
            
            if (initialised && Application.isPlaying)
                return;
            
            AddListener();
        }

        private void OnGUI()
        {
            UpdateSerialNumber();
        }

        private void OnEnable()
        {
            if (IndividualPhidgetManager.AddListener == null || initialised || serialID == 0)
                return;
            
            AddListener();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void Start()
        {
            if (IndividualPhidgetManager.AddListener == null || initialised || serialID == 0)
                return;
            
            AddListener();
        }
        
        private void AddListener()
        {
            initialised = true;
            
            if(onTagRead != null)
                IndividualPhidgetManager.AddListener?.Invoke( 
                    tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDReader, serialID == 0 ? -1: serialID
                    );
            if(onTagLost != null)
                IndividualPhidgetManager.AddListener?.Invoke( (o) => onTagLost?.Invoke(), IndividualPhidgetType.RFIDLost, serialID == 0 ? -1: serialID);
        }

        private void RemoveListeners()
        {
            initialised = false;
            
            if(onTagRead != null)
                IndividualPhidgetManager.RemoveListener?.Invoke( tag => onTagRead?.Invoke((string)tag), IndividualPhidgetType.RFIDReader, serialID == 0 ? -1: serialID);
            if(onTagLost != null)
                IndividualPhidgetManager.RemoveListener?.Invoke((o) => onTagLost?.Invoke(), IndividualPhidgetType.RFIDLost, serialID == 0 ? -1: serialID);
        }
    }
}