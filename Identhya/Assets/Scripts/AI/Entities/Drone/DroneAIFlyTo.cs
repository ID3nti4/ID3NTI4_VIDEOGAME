using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAIFlyTo : DroneComponent
{
    DroneRCController rcController;

    float targetForward, targetTargetForward, currentForward;
    float targetRoll, targetTargetRoll, currentRoll;

    public float LerpFraction = 0.5f;

    public float ToleranceRadius = 2.4f;
    float ToleranceRadiusSqr;
   // public Vector3 TargetLocation;
    public Vector3 TargetPosition;
    public Quaternion TargetRotation;

    public bool done = false;
    public bool stopAtTarget = false;

    Vector3 StartLocation;
    Quaternion StartRotation;

    void Awake()
    {
        targetForward = 0.0f;
        rcController = GetComponent<DroneRCController>();
        TargetPosition = this.transform.position;
        TargetRotation = this.transform.rotation;
        ToleranceRadiusSqr = ToleranceRadius * ToleranceRadius;
        StartLocation = this.transform.position;
        StartRotation = this.transform.rotation;
    }

    public override void Reset()
    {
        this.transform.position = StartLocation;
        this.transform.rotation = StartRotation;
        targetForward = currentForward = 0.0f;
        targetRoll = currentRoll = 0.0f;
        UpdateControl();
        stopAtTarget = false;
    }

    private void UpdateSoftVariables()
    {
        targetForward = Mathf.Lerp(targetForward, targetTargetForward, (LerpFraction * ((stopAtTarget || targetTargetForward > targetForward) ? 1.0f : 0.25f)) * Time.deltaTime / 0.016f);
        currentForward = Mathf.Lerp(currentForward, targetForward, (LerpFraction * ((stopAtTarget || targetForward > currentForward) ? 1.0f : 0.25f)) * Time.deltaTime / 0.016f);
        targetRoll = Mathf.Lerp(targetRoll, targetTargetRoll, LerpFraction * Time.deltaTime / 0.016f);
        currentRoll = Mathf.Lerp(currentRoll, targetRoll, LerpFraction * Time.deltaTime / 0.016f);
    }

    private void UpdateControl()
    {
        rcController.SetForwardAxis(currentForward);
        rcController.SetRollAxis(currentRoll);
    }

    private Vector3 flattenVector(Vector3 inVector)
    {
        inVector.y = 0;
        return inVector;
    }

    private float normalizeAngleBalanced(float inAngle)
    {
        while(inAngle > 180.0f)
        {
            inAngle -= 360.0f;
        }
        while(inAngle < -180.0f)
        {
            inAngle += 360.0f;
        }
        return inAngle;
    }

    public bool IsDone()
    {
        return done;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fromThisToTarget = flattenVector(TargetPosition - this.transform.position);
        float distanceToTargetSqr = fromThisToTarget.sqrMagnitude;
        
        if(distanceToTargetSqr < ToleranceRadiusSqr * (stopAtTarget ? 1.0f : 3.0f))
        {
            done = true;
        }
        else
        {
            UpdateSoftVariables();
            UpdateControl();

            Vector3 fromThisToTargetDirection = fromThisToTarget.normalized;
            Vector3 forward = this.transform.forward;
            float bearingAngle = Quaternion.FromToRotation(forward, fromThisToTargetDirection).eulerAngles.y;
            float balancedAngle = normalizeAngleBalanced(bearingAngle);
            float roll = Mathf.Clamp(balancedAngle / 90.0f, -1.0f, 1.0f);
            targetTargetRoll = roll;
            
            
            if(Vector3.Dot(fromThisToTargetDirection, forward) > 0.9f)
            {
                float distance = fromThisToTarget.magnitude;
                float fwd = Mathf.Clamp((distance - (ToleranceRadius/2.0f)) / 20.0f, -0.2f, 1.0f);
                if(!stopAtTarget)
                {
                    fwd = 1.0f;
                }
                targetTargetForward = fwd;
                
            }
            else
            {
                targetTargetForward = 0.0f;
            }

        }
    }

    public void StopMovement()
    {
        FlyTo(this.transform);
        targetTargetForward = 0.0f;
        targetForward = 0.0f;
        targetTargetRoll = 0.0f;
    }

    public void FlyTo(Transform tRANSFORM)
    {
        //Debug.Log("<color=blue> FlyTo: " + tRANSFORM.position + "</color>");
        done = false;
        TargetPosition = tRANSFORM.position;
        TargetRotation = tRANSFORM.rotation;
        rcController.SetTargetHeight(tRANSFORM.position.y);
    }

    public void FlyTo(Vector3 pos, Quaternion rot)
    {
        done = false;
        TargetPosition = pos;
        TargetRotation = rot;
        rcController.SetTargetHeight(pos.y);
    }
}
