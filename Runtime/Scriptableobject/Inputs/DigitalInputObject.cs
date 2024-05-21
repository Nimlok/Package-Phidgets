using System;
using Phidget22.Events;
using UnityEngine;

namespace Phidgets
{
    [CreateAssetMenu(menuName = "Phidget/Inputs/DigitalInput")]
    public class DigitalInput: PhidgetBaseObject
    {
        public Action<bool> OnStateChange;
        
        private Phidget22.DigitalInput PhidgetDigitalInput => (Phidget22.DigitalInput)Phidget;
         
        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.DigitalInput();
            PhidgetDigitalInput.StateChange += StateChange;
            PhidgetDigitalInput.IsHubPortDevice = true;
            base.InitialisePhidget();
        }

        public override void ClosePhidget()
        {
            PhidgetDigitalInput.StateChange -= StateChange;
            base.ClosePhidget();
        }

        private void StateChange(object o, DigitalInputStateChangeEventArgs e)
        {
            LogState(e.State.ToString());
            ThreadManager.instance.AddToMainThread(() => OnStateChange?.Invoke(e.State));
        }
    }
}