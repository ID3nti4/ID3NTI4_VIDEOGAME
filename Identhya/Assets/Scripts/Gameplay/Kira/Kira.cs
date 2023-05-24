using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kira : ControllableCharacter
{

    public float DefaultFriction = 0.6f;
    public float WalkSpeed = 0.15f;                 // Default walk speed.
    public float RunSpeed = 1.0f;                   // Default run speed.
    public float SprintSpeed = 2.0f;                // Default sprint speed.

    public bool _CheckWall = false;
    public bool _CheckGrounded = false;
    public float _CheckSpeed = 0.0f;

    public delegate void StateHandlerDelegate();

    public AnimatedCharacterComponent CurrentComponent = null;

    public StateHandlerDelegate OnHittingGround;

    private MoveBehaviour MoveComponent;

    private Vector3 colExtents;
    private bool isColliding;

    private void Awake()
    {
        CurrentComponent = GetComponent<MoveBehaviour>();
        MoveComponent = GetComponent<MoveBehaviour>();
        CurrentComponent = MoveComponent;
        colExtents = GetComponent<Collider>().bounds.extents;
    }

    private void Start()
    {
        HealthComponent healthComponent = GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.OnCharacterDied += OnKiraDied;
        }
    }

    private void OnKiraDied(GameObject Source)
    {
        DeactivateAllComponents();
        CurrentComponent = null;
    }

    public void SetPlayerBlocked(bool blocked)
    {
        if(blocked)
        {
            DeactivateAllComponents();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Animator>().SetFloat("Speed", 0.0f);
            CurrentComponent = null;
            IsBlocked = true;
            InteractComponent interact = GetComponent<InteractComponent>();
            if(interact != null)
            {
                interact.isBlocked = true;
            }
        }
        else
        {
            InteractComponent interact = GetComponent<InteractComponent>();
            if (interact != null)
            {
                interact.isBlocked = false;
            }
            ActivateMovement(true);
            IsBlocked = false;
        }
    }

    public void ActivateMovement(bool ForceCanMove = false)
    {
        DeactivateAllComponents();
        CurrentComponent = MoveComponent;
        if(ForceCanMove)
        {
            MoveComponent.SetCanMove();
        }
    }

    private void OnClimbFinished()
    {
        DeactivateAllComponents();
        CurrentComponent = MoveComponent;
    }

    private void OnJumpFinished()
    {
        DeactivateAllComponents();
        CurrentComponent = MoveComponent;
        MoveComponent.SetCanMove();
    }

    private void OnFlyFinished()
    {
        DeactivateAllComponents();
        CurrentComponent = MoveComponent;
        MoveComponent.SetCanMove();
    }

    private void ActivateComponent<T>(AnimatedCharacterComponent.OnComponentFinishedDelegate onComponentFinishAction) where T:AnimatedCharacterComponent
    {
        CurrentComponent = GetComponent<T>();
        CurrentComponent.OnComponentActivate();
        CurrentComponent.OnComponentFinished = onComponentFinishAction;
    }
       
    private void DeactivateAllComponents()
    {
        foreach (AnimatedCharacterComponent component in GetComponents<AnimatedCharacterComponent>())
        {
            component.OnComponentDeactivate();
        }
    }

    private void FixedUpdate()
    {
        CurrentComponent?.ComponentUpdate(Time.deltaTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        _CheckWall = IsInFrontOfWall();
        _CheckGrounded = IsGrounded();
        _CheckSpeed = GetComponent<Rigidbody>().velocity.magnitude;

        CharacterInput downinput = downInput.GetInput();

        foreach (ControllableCharacterComponent component in GetComponents<ControllableCharacterComponent>())
        {
            component.ComponentInputUpdate(downinput);
        }
        
    }

    public void ActivateOneComponent<T>() where T : AnimatedCharacterComponent
    {
        T component = GetComponent<T>();
        DeactivateAllComponents();
        component.OnComponentActivate();
        component.OnComponentFinished = null;
        CurrentComponent = component;
    }

    [SerializeField]
    bool isGrounded = true;
    public bool IsGrounded()
    {
        if ((CurrentComponent is Jump) && GetComponent<Rigidbody>().velocity.y > 0.0f) return false;
        Ray ray = new Ray(this.transform.position + Vector3.up * 2 * colExtents.x, Vector3.down);
        return Physics.SphereCast(ray, colExtents.x, colExtents.x + 0.4f);
        //return isGrounded;
    }

    public void ForceUngrounded()
    {
        isGrounded = false;
    }

    public bool IsInFrontOfWall()
    {
        RaycastHit hit;
        _CheckWall = Physics.Raycast(this.transform.position + Vector3.up * 1.7f - this.transform.forward * 0.2f, this.transform.forward, out hit, 2.0f);
        return _CheckWall;
    }

    public bool IsColliding()
    {
        return isColliding;
    }

    public void KillFriction()
    {
        SetFriction(0.0f);
    }

    public void SetDefaultFriction()
    {
        SetFriction(DefaultFriction);
    }

    public void SetFriction(float value)
    {
        GetComponent<CapsuleCollider>().material.frictionCombine = value > 0.0f ? PhysicMaterialCombine.Maximum : PhysicMaterialCombine.Minimum;
        GetComponent<CapsuleCollider>().material.dynamicFriction = value;
        GetComponent<CapsuleCollider>().material.staticFriction = value;
    }

    private void OnCollisionStay(Collision collision)
    {
        isColliding = true;
        // Slide on vertical obstacles
        if (collision.GetContact(0).normal.y <= 0.1f)
        {
            KillFriction();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        SetDefaultFriction();
    }

    public CharacterComponent GetActiveComponent()
    {
        return CurrentComponent;
    }

    public void DisablePhysics()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        Collider col = GetComponentInChildren<Collider>();
        col.enabled = false;
    }

    public void EnablePhysics()
    {
        Rigidbody rb = GetComponentInChildren<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        Collider col = GetComponentInChildren<Collider>();
        col.enabled = true;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            OnHittingGround?.Invoke();
        }
    }*/


}
