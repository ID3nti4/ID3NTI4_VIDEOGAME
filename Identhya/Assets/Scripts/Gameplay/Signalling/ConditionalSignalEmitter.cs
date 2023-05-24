using UnityEngine;

public class ConditionalSignalEmitter : SignalEmitterInterface
{
    public SignalInfo info;
    public ConditionalInterface condition;
    public override void ReceiveSignalFromPrev(SignalInfo info)
    {
        Debug.Log("Checking condition...");
        if (condition.checkCondition())
        {
            Debug.Log("Emitting conditional signal");
            EmitSignalToNext?.Invoke(info);
        }
    }
}
