using Nimlok.Phidgets;
using Phidget22.Events;

public class RFIDData
{
    public string tag;
    public bool lost;

    public RFIDData(string tag, bool lost)
    {
        this.tag = tag;
        this.lost = lost;
    }
}

public class RFIDVint : BasePhidget
{
    private Phidget22.RFID PhidgetRFIDVint => (Phidget22.RFID)Phidget;

    public override PhidgetInputType PhidgetInputType => PhidgetInputType.RFIDVint;

    public override void InitialisePhidget()
    {
        Phidget = new Phidget22.RFID();
        PhidgetRFIDVint.Tag += StateChange;
        PhidgetRFIDVint.TagLost += StateChange;
        PhidgetRFIDVint.IsHubPortDevice = true;
        base.InitialisePhidget();
    }

    private void StateChange(object sender, RFIDTagLostEventArgs e)
    {
        LogState(e.Tag);
        var data = new RFIDData(e.Tag, true);
        ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(data));
    }

    private void StateChange(object sender, RFIDTagEventArgs e)
    {
        LogState(e.Tag);
        var data = new RFIDData(e.Tag, false);
        ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(data));
    }

    public override void ClosePhidget()
    {
        if (PhidgetRFIDVint == null)
            return;
            
        PhidgetRFIDVint.Tag -= StateChange;
        base.ClosePhidget();
    }
        
    public override void TriggerPhidget(object value = null)
    {
        base.TriggerPhidget(value);
        if (value != null) PhidgetRFIDVint.AntennaEnabled = (bool)value;
    }
}
