using Nimlok.Phidgets;
using Phidget22;
using UnityEngine;

public class Relay : BasePhidget
{
    private DigitalOutput GetRelay => (DigitalOutput)Phidget;
    
    public override void TriggerPhidget(object target)
    {
        if (target == null)
        {
            Debug.Log($"Target Missing");
            return;
        }
            
        TriggerRelay(target);
        base.TriggerPhidget(target);
    }

    private void TriggerRelay(object target)
    {
        try {
            GetRelay.State = (bool)target;
        } catch (PhidgetException ex) 
        {
            Debug.LogError($"Failed to Set Relay: {ex}");
        }
    }
}
