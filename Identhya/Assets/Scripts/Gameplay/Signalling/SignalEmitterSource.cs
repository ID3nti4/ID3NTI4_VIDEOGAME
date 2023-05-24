using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEmitterSource : MonoBehaviour
{
    public delegate void EmitSignalDelegate(SignalInfo info);

    public EmitSignalDelegate EmitSignalToNext;
}
