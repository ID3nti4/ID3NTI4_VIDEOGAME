using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SignalEmitterTrigger : SignalEmitterSource
{
    public SignalInfo info;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Emit");
            EmitSignalToNext?.Invoke(info);
        }
    }
}
