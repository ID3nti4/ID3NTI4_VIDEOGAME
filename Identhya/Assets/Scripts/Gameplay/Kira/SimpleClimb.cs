using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClimb : AnimatedCharacterComponent
{
    [SerializeField] string ClimbAnimationKey = "IsClimbing";
    [SerializeField] string TopReachedAnimationKey = "TopReached";
    [SerializeField] string ClimbSpeedAnimationKey = "ClimbSpeed";
    [SerializeField] string JustFallAnimationKey = "JustFall";
    [SerializeField] string JumpAnimationKey = "Jump";
    [SerializeField] float WallJumpSpeed = 3.0f;

    private Kira kira;

    public bool canClimbDown = false;

    public bool canclimbUp = false;

    public bool isClimbing = false;

    bool exitTop = false;

    public float climbStartY;

    const float minClimb = 0.25f;

    public bool ReachedEnd = false;

    public GameObject rightGloveLight, leftGloveLight;

    public Collider climbCollider = null;

    public bool Enabled = true;

    public EnergySystem energySystem;

    private ParticleSystem leftGloveEffect, rightGloveEffect;
    private bool glovesEffect = false;

    private new void Awake()
    {
        base.Awake();
        kira = GetComponent<Kira>();
    }

    private void Start()
    {
        leftGloveEffect = GameObject.Find("LeftGloveEffect").GetComponent<ParticleSystem>();
        rightGloveEffect = GameObject.Find("RightGloveEffect").GetComponent<ParticleSystem>();

    }

    private void OnClimbFinished(bool ForceEnableMovement)
    {
        kira.ActivateMovement(ForceEnableMovement);
    }

    public override bool CanActivate()
    {

        if (!Enabled) return false;
        if (isClimbing) return false;
        if (!kira.IsInFrontOfWall()) return false;
        return climbCollider != null;
    }

    private bool CanGrabSurface()
    {
        return Vector3.Dot(-climbCollider.gameObject.transform.forward, this.transform.forward) > 0.25f;
    }

    public override void ComponentInputUpdate(CharacterInput input)
    {
        energySystem = GetComponent<EnergySystem>();
        if (input.Interact && CanActivate() && !isClimbing && CanGrabSurface() && FindObjectOfType<InventoryController>().HasGloves())
        {
            if(energySystem.currentEnergy >= 5)
            {
                isClimbing = true;
                energySystem.climbing = true;
                kira.ActivateOneComponent<SimpleClimb>();
                return;
            }
            else if(energySystem.currentEnergy < 5)
            {
                energySystem.PlayEnergyBarBlink();
            }
        }

        /*if(input.Interact && isClimbing && !ReachedEnd)
        {
            animator.SetBool(JustFallAnimationKey, true);
            isClimbing = false;
            OnClimbFinished(true);
        }*/

        if(input.Jump && isClimbing && !ReachedEnd)
        {
            animator.SetBool(JustFallAnimationKey, true);
            ReachedEnd = true;
            StartCoroutine(JumpBackwards());
        }

        if (isClimbing)
        {
            if (!glovesEffect)
            {
                StartCoroutine("StartLeftGloveEffect");
                StartCoroutine("StartRightGloveEffect");
                glovesEffect = true;
            }

            if(energySystem.currentEnergy <= 0.5)
            {
                Enabled = false;
                OnClimbFinished(true);
                animator.SetBool(JustFallAnimationKey, true);
                isClimbing = false;
                energySystem.climbing = false;
                energySystem.PlayEnergyBarBlink();
            }
        }
        else
        {
            if (glovesEffect)
            {
                StartCoroutine("StopLeftGloveEffect");
                StartCoroutine("StopRightGloveEffect");
                glovesEffect = false;
            }
        }

    }

    private IEnumerator StartLeftGloveEffect()
    {
        Debug.Log("ENTRAAAAAAA");
        for (float scale = 0f; scale <= 0.2f; scale += 0.001f)
        {
            leftGloveLight.transform.localScale = new Vector3 (scale, scale, scale);
            yield return null;
        }
    }

    private IEnumerator StartRightGloveEffect()
    {
        for (float scale = 0f; scale <= 0.2f; scale += 0.001f)
        {
            rightGloveLight.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }

    private IEnumerator StopLeftGloveEffect()
    {
        yield return new WaitForSeconds(1f);
        for (float scale = 0.2f; scale >= 0f; scale -= 0.001f)
        {
            leftGloveLight.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }

    private IEnumerator StopRightGloveEffect()
    {
        yield return new WaitForSeconds(1f);
        for (float scale = 0.2f; scale >= 0f; scale -= 0.001f)
        {
            rightGloveLight.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }

    IEnumerator JumpBackwards()
    {
        GetComponent<Jump>().Enabled = false; // @TODO : Create a higher level logic to manage intercomponent interactions; Climb should not know anything about Jump
        Enabled = false;
        yield return StartCoroutine(OrientTowardsClimbSurface(2.5f, true));
        OnClimbFinished(true);
        isClimbing = false;
        energySystem.climbing = false;
        Jump jump = GetComponent<Jump>();
        if(jump != null)
        {
            Vector3 flatVelocity = new Vector3(kira.transform.forward.x, 0, kira.transform.forward.z);
            jump.ForceJump(kira.transform.forward * WallJumpSpeed);
        }
        yield return new WaitForSeconds(1.0f);
        Enabled = true;
        GetComponent<Jump>().Enabled = true; // @TODO : Create a higher level logic to manage intercomponent interactions; Climb should not know anything about Jump
    }

    public override void OnComponentActivate()
    {
        StartCoroutine(OrientTowardsClimbSurface());

        BoomerangController boomerangController = GetComponent<BoomerangController>();
        if(boomerangController != null)
        {
            boomerangController.PutBack();
        }

        StaffController staffController = GetComponent<StaffController>();
        if(staffController != null)
        {
            staffController.PutBack();
        }
        
        animator.SetBool(ClimbAnimationKey, true);
        animator.SetFloat(ClimbSpeedAnimationKey, 0.0f);
        animator.SetBool(JustFallAnimationKey, false);
        climbStartY = kira.transform.position.y;
        canClimbDown = false;
        rigidbody.useGravity = false;
        canclimbUp = false;
        exitTop = false;
        ReachedEnd = false;
    }

    public override void OnComponentDeactivate()
    {
        animator.SetBool(ClimbAnimationKey, false);
        animator.SetFloat(ClimbSpeedAnimationKey, 0.0f);
    }

    public override void ComponentUpdate(float DeltaTime)
    {
        bool done = false;

        float ClimbSpeed = kira.pressInput.GetInput().MovementVertical;
        animator.SetFloat(ClimbSpeedAnimationKey, ClimbSpeed);

        if(kira.transform.position.y > climbStartY + minClimb)
        {
            canclimbUp = true;
        }

        if(ClimbSpeed < 0.0f)
        {
            canClimbDown = true;
        }

        if(canClimbDown && kira.transform.position.y <= climbStartY)
        {
            Debug.Log("Bottom finish climb");
            animator.SetBool(TopReachedAnimationKey, false);
            ReachedEnd = true;
            isClimbing = false;
            energySystem.climbing = false;
            done = true;
        }

        if(canclimbUp && (!exitTop) && !kira.IsInFrontOfWall())
        {
            Debug.Log("Top finish climb: ");
            GetComponent<Jump>().Enabled = false; // @TODO : Create a higher level logic to manage intercomponent interactions; Climb should not know anything about Jump
            Enabled = false;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            animator.SetBool(TopReachedAnimationKey, true);
            ReachedEnd = true;
            isClimbing = false;
            energySystem.climbing = false;
            done = true;
           
            exitTop = true;
            //StartCoroutine(FinishClimb(1.0f, 0.35f));
        }

        if(done)
        {
            animator.SetBool(ClimbAnimationKey, false);
            isClimbing = false;
            energySystem.climbing = false;
            OnClimbFinished(false);
        }
    }
    /*
    IEnumerator FinishClimb(float up, float forward)
    {
        Vector3 FinalLocation = kira.transform.position + kira.transform.up * up + kira.transform.forward * forward;
        Vector3 Location = kira.transform.position;

        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
        animator.SetBool(ClimbAnimationKey, false);

        yield return new WaitForEndOfFrame();
        
        //while((FinalLocation-Location).magnitude > 0.03f)
        //{
        //    Debug.Log(". " + (FinalLocation - Location).magnitude);
        //    Location = Vector3.Lerp(Location, FinalLocation, 7.0f * Time.deltaTime);
        //    kira.transform.position = Location;
        //    yield return new WaitForEndOfFrame();
        //}

        ReachedEnd = true;
        rigidbody.isKinematic = false;
        OnClimbFinished(false);
    }*/

    IEnumerator OrientTowardsClimbSurface(float SpeedMultiplier=1.0f, bool reverseDirection = false)
    {
        const float tolerance = 0.01f;

        rigidbody.useGravity = false;

        Vector3 climbForward = (reverseDirection ? 1.0f : -1.0f) * GetClimbCollider().transform.forward;
        climbForward.y = 0.0f;

        Vector3 kiraForward = kira.transform.forward;
        kiraForward.y = 0.0f;

        while (Vector3.Dot(climbForward, kiraForward) < (1.0f - tolerance))
        {
            kiraForward = Vector3.Lerp(kiraForward, climbForward, 10.0f * Time.deltaTime * SpeedMultiplier);
            float angle = Quaternion.LookRotation(kiraForward).eulerAngles.y;
            kira.transform.rotation = Quaternion.Euler(0, angle, 0);
            kiraForward = kira.transform.forward;
            
            yield return new WaitForEndOfFrame();
         
        }
    }

    public Collider GetClimbCollider()
    {
        return climbCollider;
    }

    bool t = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            t = !t;
            Time.timeScale = t ? 0.1f : 1.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Climbable")
        {
            climbCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Climbable")
        {
            climbCollider = null;
        }
    }
}
