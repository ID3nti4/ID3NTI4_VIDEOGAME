using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parent
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public Parent()
    {
        Position = Vector3.zero;
        Rotation = new Quaternion();
        Scale = Vector3.one;
    }

    public Parent(Vector3 Position, Quaternion Rotation, Vector3 Scale)
    {
        this.Position = Position;
        this.Rotation = Rotation;
        this.Scale = Scale;
    }

    public static Parent Lerp(Parent one, Parent other, float blend)
    {
        return new Parent(Vector3.Lerp(one.Position, other.Position, blend), Quaternion.Lerp(one.Rotation, other.Rotation, blend), Vector3.Lerp(one.Scale, other.Scale, blend));
    }
}

public class ParentConstraint : MonoBehaviour
{
    public ParentSource ParentSource;
    public bool Enabled = true;
    private void Update()
    {
        if (Enabled)
        {
            Parent p = ParentSource.ObtainParent();
            this.transform.position = p.Position;
            this.transform.rotation = p.Rotation;
            this.transform.localScale = p.Scale;
        }
    }
}
