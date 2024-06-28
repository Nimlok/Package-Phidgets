using System;
using Phidget22.Events;

namespace Phidgets
{
    public class DigitalInput: BasePhidget
    {
        public Action<bool> OnStateChange;
        
        private Phidget22.DigitalInput PhidgetDigitalInput => (Phidget22.DigitalInput)Phidget;

        public override PhidgetInputType PhidgetInputType => PhidgetInputType.DigitalInput;

        public override void InitialisePhidget()
        {
            Phidget = new Phidget22.DigitalInput();
            PhidgetDigitalInput.StateChange += StateChange;
            PhidgetDigitalInput.IsHubPortDevice = true;
            base.InitialisePhidget();
        }

        public override void ClosePhidget()
        {
            if (PhidgetDigitalInput == null)
                return;
            
            PhidgetDigitalInput.StateChange -= StateChange;
            base.ClosePhidget();
        }
        
        public override void TriggerPhidget(object value = null)
        {
            base.TriggerPhidget(value);
            OnStateChange?.Invoke(true);
        }
        
        private void StateChange(object o, DigitalInputStateChangeEventArgs e)
        {
            LogState(e.State.ToString());
            ThreadManager.instance.AddToMainThread(() => OnStateChange?.Invoke(e.State));
        }
    }
}