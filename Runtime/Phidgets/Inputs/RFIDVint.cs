using Nimlok.Phidgets;
using Phidget22;
using Phidget22.Events;

public class RFIDData
{
    public string tag;
    public bool lost;
    public int hub;
    public int port;

    public RFIDData(string tag, bool lost, int hub, int port)
    {
        this.tag = tag;
        this.lost = lost;
    }
}

public class RFIDVint : BasePhidget
{
    private Phidget22.RFID PhidgetRfidVint => (Phidget22.RFID)Phidget;

    public override PhidgetInputType PhidgetInputType => PhidgetInputType.RFIDVint;
    
    public Phidget22.RFID GetBasePhidget => (Phidget22.RFID)Phidget;
    
    public override void InitialisePhidget()
    {
        Phidget = new Phidget22.RFID();
        PhidgetRfidVint.Tag += StateChange;
        PhidgetRfidVint.TagLost += StateChange;
        base.InitialisePhidget();
    }

    private void StateChange(object sender, RFIDTagLostEventArgs e)
    {
        LogState(e.Tag);
        var data = new RFIDData(e.Tag, true, serialID, port);
        ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(data));
    }

    private void StateChange(object sender, RFIDTagEventArgs e)
    {
        LogState(e.Tag);
        var data = new RFIDData(e.Tag, false, serialID, port);
        ThreadManager.instance.AddToMainThread(() => onStateChange?.Invoke(data));
    }

    public override void ClosePhidget()
    {
        if (PhidgetRfidVint == null)
            return;
            
        PhidgetRfidVint.Tag -= StateChange;
        base.ClosePhidget();
    }
        
    public override void TriggerPhidget(object value = null)
    {
        base.TriggerPhidget(value);
        if (value != null) PhidgetRfidVint.AntennaEnabled = (bool)value;
    }
}
