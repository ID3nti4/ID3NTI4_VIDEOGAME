using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTriggerOnSignal : SignalEmitterInterface
{
    public Collider _collider;

    public override void ReceiveSignalFromPrev(SignalInfo info)
    {
        _collider.enabled = false;
    }
}
