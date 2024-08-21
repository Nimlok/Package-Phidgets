using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nimlok.Phidgets
{
    [Serializable]
    public struct PhidgetData
    {
        [ReadOnly] public int port;
        [FormerlySerializedAs("type")] public PhidgetInputType inputType;
        public bool debug;
        [HideInInspector] public int serial;
        
       public BasePhidget GetPhidget()
       {
           var newPhidget = GetPhidgetType.GetType(inputType);    

            if (newPhidget == null) 
                return null;
            
            newPhidget.port = port;
            newPhidget.SetSerial = serial;
            newPhidget.debug = debug;
            return newPhidget;
        }
    }
}