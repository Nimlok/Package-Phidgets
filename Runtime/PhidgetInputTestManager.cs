using System;
using Nimlok.Phidgets.EventComponents;
using UnityEngine;

namespace Nimlok.Phidgets
{
    [Serializable]
    public class PhidgetKeyData
    {
        public BasePhidgetData BasePhidgetData;
        public KeyCode KeyCode;
    }
    
    public class PhidgetInputTestManager: MonoBehaviour
    {
        [SerializeField] private PhidgetKeyData[] phidgetKeyDatas;
        
        private void Update()
        {
            foreach (var phidgetKeyData in phidgetKeyDatas)
            {
                if (Input.GetKeyDown(phidgetKeyData.KeyCode))
                {
                    HubEventsManager.ActivatePhidget?.Invoke(null, phidgetKeyData.BasePhidgetData.port, phidgetKeyData.BasePhidgetData.GetSerialNumber());
                }
            }
        }
    }
}