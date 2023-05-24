using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRCController : DroneComponent
{
    public Vector3 Velocity;
    Rigidbody rb;

    float pitch, rawpitch;
    float roll, rawroll;
    float yaw;
    float yawSpeed = 0.0f;

    float TargetHeight;

    public float HeightTension = 4.0f;

    const float PitchFactor = 20.0f;
    const float RollFactor = 40.0f;

    public float PitchSpeed = 20.0f;
    public float RollLinearSpeed = 2.0f;
    public float RollAngularSpeed = 80.0f;

    public float TurbulenceStrength = 1.0f;

    public Transform DroneTransform;

    GameObject captureObj;

    public bool possessed = false;

    private void Start()
    {
        yaw = DroneTransform.rotation.eulerAngles.y;
        yawSpeed = 0;
        rb = GetComponent<Rigidbody>();
        TargetHeight = this.transform.position.y;
    }

    public override void Reset()
    {
        captureObj = null;
       // GetComponent<EstadoPersecucion>().isCapturing = false;
    }

    public void SetRollAxis(float value)
    {
        value = -value;
        rawroll = value;
        roll = RollFactor * value;
        yawSpeed = -value * RollAngularSpeed;
    }
    
    public void SetForwardAxis(float value)
    {
        rawpitch = value;
        pitch = value * PitchFactor;
    }

    private void UpdateRotations()
    {
        DroneTransform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        yaw += yawSpeed * Time.deltaTime;
        
    }

    private void UpdateVelocity()
    {
        Velocity = DroneTransform.forward * rawpitch * PitchSpeed;
        Velocity.y = ComputeVerticalSpeed();
    }

    private float ComputeVerticalSpeed()
    {
        float Tension = (TargetHeight - this.transform.position.y) * HeightTension;
        return Tension;
    }

  
    public void SetTargetHeight(float TargetHeight)
    {
        this.TargetHeight = TargetHeight;
    }

    private void UpdatePosition()
    {
        //DroneTransform.position += Velocity * Time.deltaTime;
        rb.velocity = Velocity;
    }

    public void StickToTransform(GameObject target)
    {
        captureObj = target;
    }

    private void Update()
    {
        if (!captureObj)
        {
            if (possessed)
            {
                //SetForwardAxis(Input.GetAxis("Vertical"));
                //SetRollAxis(Input.GetAxis("Horizontal"));
                SetRollAxis(-1.0f);
            }
            UpdateRotations();
            UpdateVelocity();
            UpdatePosition();
        }
        else
        {
            transform.position = captureObj.transform.position;
            transform.rotation = captureObj.transform.rotation;
        }        
    }
}
