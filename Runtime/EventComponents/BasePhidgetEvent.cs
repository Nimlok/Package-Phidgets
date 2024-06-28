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
    public class BasePhidgetEvent: MonoBehaviour
    {
        public BasePhidgetData basePhidgetData;
    }
}