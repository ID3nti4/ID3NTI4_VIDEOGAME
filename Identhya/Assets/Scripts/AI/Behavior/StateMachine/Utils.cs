using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 FindNavigableLocation(Vector3 CharacterPosition, Vector3 CharacterForward, float MinRange, float MaxRange, float minAngle, float maxAngle)
    {
        float maxDotProduct = Mathf.Cos(Mathf.Deg2Rad * minAngle);
        float minDotProduct = Mathf.Cos(Mathf.Deg2Rad * maxAngle);
        float dotProduct = 0.0f;
        Vector3 Displacement = Vector3.zero;


        float DisplRadius = Random.Range(MinRange, MaxRange);
        float Angle = Random.Range(minAngle, maxAngle);
        if(TryProbability(0.5f))
        {
            Angle = -Angle;
        }

        Vector3 Direction = CharacterForward;
        Quaternion Rot = Quaternion.Euler(0, Angle, 0);
        Direction = Rot * Direction;

        Displacement = Direction * DisplRadius;

        //Debug.Log(minAngle + " -> " + maxAngle + "    Chosen angle: " + Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(Displacement.normalized, CharacterForward)));

        return CharacterPosition + Displacement;
    }

    public static bool TryProbability(float prob)
    {
        return Random.Range(0.0f, 1.0f) <= prob;
    }
}
