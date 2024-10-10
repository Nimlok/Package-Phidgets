using Nimlok.Phidgets;
using Nimlok.Phidgets.IndividualPhidgets;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace EventComponents
{
    public class SpatialEvent: MonoBehaviour
    {
        [OnValueChanged("UpdateSerialNumber")]
        public SpatialPrecision spatialPrecision;
        
        public int SerialID;
        
        [Space]
        [SerializeField] private UnityEvent<double[]>  onAccelerometer;
        [SerializeField] private UnityEvent<double[]>  onRotation;
        
        
        private bool initialised;
        
        private void UpdateSerialNumber()
        {
            if (spatialPrecision == null)
                return;

            SerialID = spatialPrecision.serialID;
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
            if(onAccelerometer != null)
                IndividualPhidgetManager.AddListener?.Invoke( tag => onAccelerometer?.Invoke((double[])tag), 
                    IndividualPhidgetType.SpatialAccelormeter, SerialID == 0 ? -1: SerialID);
            if(onRotation != null)
                IndividualPhidgetManager.AddListener?.Invoke( (o) => onRotation?.Invoke((double[])o), IndividualPhidgetType.SpatialRotation, SerialID == 0 ? -1: SerialID);
        }

        private void RemoveListeners()
        {
            if(onAccelerometer != null)
                IndividualPhidgetManager.AddListener?.Invoke( tag => onAccelerometer?.Invoke((double[])tag), 
                    IndividualPhidgetType.SpatialAccelormeter, SerialID == 0 ? -1: SerialID);
            if(onRotation != null)
                IndividualPhidgetManager.AddListener?.Invoke( (o) => onRotation?.Invoke((double[])o), IndividualPhidgetType.SpatialRotation, SerialID == 0 ? -1: SerialID);
        }
    }
}