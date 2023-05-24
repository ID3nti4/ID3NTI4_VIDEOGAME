using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public delegate void OnBoomerageReturnDelegate();
    public OnBoomerageReturnDelegate OnBoomerangeReturn;


    Vector3 StartingVelocity;
    Vector3 TargetPoint;
    Vector3 TargetVelocity;
    Vector3 ReturnVelocity;
    public float ThrowLateralSpeedFraction = 0.65f;
    public float Speed = 20.0f;
    // Start is called before the first frame update
   
    public void Launch(Vector3 StartPoint, Vector3 TargetPoint, Vector3 StartingVelocity, Vector3 TargetVelocity, Vector3 ReturnVelocity, Transform returnPoint)
    {
        FindObjectOfType<AimBehaviourBasic>().isShooting = true;
        StartCoroutine(BoomerangFlight(StartPoint, TargetPoint, StartingVelocity, TargetVelocity, ReturnVelocity, returnPoint));
    }
    
    IEnumerator BoomerangFlight(Vector3 StartPoint, Vector3 TargetPoint, Vector3 StartingVelocity, Vector3 TargetVelocity, Vector3 ReturnVelocity, Transform returnPoint)
    {
        Vector3 LocalZ = (TargetPoint - StartPoint).normalized;
        float DistanceToTarget = (TargetPoint - StartPoint).magnitude;
        float TimeToTarget = DistanceToTarget / Speed;

        //Vector3 StartingAcceleration = (TargetVelocity - StartingVelocity) / TimeToTarget;
        //Vector3 ReturningAcceleration = (ReturnVelocity - TargetVelocity) / TimeToTarget;
        //Vector3 CurrentVelocity = StartingVelocity;
        Vector3 CurrentPosition = StartPoint;
        Vector3 VelZ = LocalZ * 2.0f * DistanceToTarget / TimeToTarget;
        Vector3 AccelZ = -VelZ / TimeToTarget;
        Vector3 CurrentZVelocity = VelZ;
        Vector3 StartingZAcceleration = AccelZ;

        Vector3 LocalX = Vector3.Cross(Vector3.up, LocalZ);
        Vector3 VelX = (DistanceToTarget / TimeToTarget) * LocalX * ThrowLateralSpeedFraction;
        Vector3 AccelX = -2.0f * VelX / TimeToTarget;
        Vector3 CurrentXVelocity = VelX;
        Vector3 StartingXAcceleration = AccelX;

        Vector3 ReturnCurrentPosition;

        float elapsed = 0.0f;
        while(elapsed < TimeToTarget)
        {
            CurrentPosition += (CurrentZVelocity * Time.deltaTime + CurrentXVelocity * Time.deltaTime);
            CurrentZVelocity += StartingZAcceleration * Time.deltaTime;
            CurrentXVelocity += StartingXAcceleration * Time.deltaTime;
            this.transform.position = CurrentPosition;
            yield return new WaitForEndOfFrame();
            elapsed += Time.deltaTime;
        }
        CurrentPosition = TargetPoint;
        CurrentZVelocity = Vector3.zero;
        elapsed = 0.0f;
        while(elapsed < TimeToTarget)
        {
            CurrentPosition += (CurrentZVelocity * Time.deltaTime + CurrentXVelocity * Time.deltaTime);
            CurrentZVelocity += StartingZAcceleration * Time.deltaTime;
            CurrentXVelocity -= StartingXAcceleration * Time.deltaTime;
            float DistanceToReturnPoint = (CurrentPosition - returnPoint.position).magnitude;
            ReturnCurrentPosition = Vector3.Lerp(CurrentPosition, returnPoint.position, Mathf.Exp((elapsed - TimeToTarget)*1.4f));
            this.transform.position = ReturnCurrentPosition;
            yield return new WaitForEndOfFrame();
            elapsed += Time.deltaTime;
        }
        OnBoomerangeReturn?.Invoke();
    }

}
