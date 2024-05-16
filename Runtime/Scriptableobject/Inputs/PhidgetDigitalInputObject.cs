using System;
using Phidget22;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/DigitalInput")]
    public class PhidgetDigitalInput: PhidgetBaseObject
    {
        public Action<bool> OnStateChange;
        
        private DigitalInput DigitalInput => (DigitalInput)Phidget;
         
        public override void InitialisePhidget()
        {
            Phidget = new DigitalInput();
            DigitalInput.StateChange += StateChange;
            DigitalInput.IsHubPortDevice = true;
            base.InitialisePhidget();
        }

        public override void ClosePhidget()
        {
            DigitalInput.StateChange -= StateChange;
            base.ClosePhidget();
        }

        private void StateChange(object o, DigitalInputStateChangeEventArgs e)
        {
            LogState(e.State.ToString());
            ThreadManager.instance.AddToMainThread(() => OnStateChange?.Invoke(e.State));
        }
    }
}