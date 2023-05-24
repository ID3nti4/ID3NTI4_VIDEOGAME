using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectOnSignal : SignalEmitterInterface
{
    public GameObject _gameObject;

    public override void ReceiveSignalFromPrev(SignalInfo info)
    {
        Destroy(_gameObject);
    }
}
