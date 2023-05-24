using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSourceBlender : ParentSource
{
    public ParentSource SourceA;
    public ParentSource SourceB;
    public float Blend = 0.0f;

    public override Parent ObtainParent()
    {
        return Parent.Lerp(SourceA.ObtainParent(), SourceB.ObtainParent(), Blend);
    }
}
