using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/VoltageRatio")]
    public class PhidgetVoltageObject: PhidgetBaseObject
    {
        private float voltageRatio;
        protected VoltageRatioInput voltageRatioInput => (VoltageRatioInput)Phidget;
        
        public Action<float> voltageChanged;
        
        public override void InitialisePhidget()
        {
            Phidget = new VoltageRatioInput();
            Phidget.IsHubPortDevice = true;
            voltageRatioInput.VoltageRatioChange += VoltageRatioChange;
            base.InitialisePhidget();
        }

        public override void ClosePhidget()
        {
            voltageRatioInput.VoltageRatioChange -= VoltageRatioChange;
            base.ClosePhidget();
        }

        private void VoltageRatioChange(object sender, VoltageRatioInputVoltageRatioChangeEventArgs args)
        {
            voltageRatio = (float)args.VoltageRatio;
            LogState(voltageRatio.ToString());
            ThreadManager.instance.AddToMainThread(() => voltageChanged?.Invoke(voltageRatio));
        }
    }
}