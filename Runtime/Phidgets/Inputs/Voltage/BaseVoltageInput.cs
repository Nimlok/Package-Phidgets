using Phidget22;
using Phidget22.Events;

namespace Phidgets
{
    public class BaseVoltageInput: BasePhidget
    {
        public override PhidgetInputType PhidgetInputType => PhidgetInputType.VoltageRatio;
        
        protected VoltageRatioInput GetVoltageRatioInput => (VoltageRatioInput)Phidget;
        
        public override void InitialisePhidget()
        {
            Phidget = new VoltageRatioInput();
            Phidget.IsHubPortDevice = true;
            GetVoltageRatioInput.VoltageRatioChange += GetVoltageRatioChange;
            base.InitialisePhidget();
        }

        public override void ClosePhidget()
        {
            GetVoltageRatioInput.VoltageRatioChange -= GetVoltageRatioChange;
            base.ClosePhidget();
        }

        private void GetVoltageRatioChange(object sender, VoltageRatioInputVoltageRatioChangeEventArgs args)
        {
            var voltageRatio = (float)args.VoltageRatio;
            LogState(voltageRatio.ToString());
            ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(voltageRatio));
        }
    }
}