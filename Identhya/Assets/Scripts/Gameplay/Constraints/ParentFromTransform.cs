using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentFromTransform : ParentSource
{
    public Transform ParentTransform;
    public override Parent ObtainParent()
    {
        return new Parent(ParentTransform.position, ParentTransform.rotation, ParentTransform.localScale);
    }
}
