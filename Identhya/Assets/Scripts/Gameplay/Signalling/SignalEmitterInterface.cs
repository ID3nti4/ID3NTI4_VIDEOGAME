[System.Serializable]
public class SignalInfo
{
    public bool boolValue;
    public string stringValue;
    public int intValue;
    public float floatValue;
}

public abstract class SignalEmitterInterface : SignalEmitterSource
{
    public SignalEmitterSource signalSource_N;
    
    protected void Start()
    {
        if(signalSource_N != null)
        {
            signalSource_N.EmitSignalToNext += ReceiveSignalFromPrev;
        }
    }

    public abstract void ReceiveSignalFromPrev(SignalInfo info);


}
