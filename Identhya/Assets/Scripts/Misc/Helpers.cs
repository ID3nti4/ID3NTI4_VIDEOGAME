using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static Vector3 GetFlatPerpendicular(Vector3 inVector)
    {
        Vector3 result = inVector;
        result.x = inVector.z;
        result.z = -inVector.x;
        result.y = inVector.y;
        return result;
    }
}
