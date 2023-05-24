public class MultiSignal : SignalEmitterInterface
{
    public SignalEmitterInterface[] outlets;

    public override void ReceiveSignalFromPrev(SignalInfo info)
    {
        foreach(SignalEmitterInterface outlet in outlets)
        {
            outlet.EmitSignalToNext?.Invoke(info);
        }
    }
}
