using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModifier
{
    public bool HasGloves = false;
    public bool HasBoots = false;
    public bool HasStaff = false;
}


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(UnityInputCharacterControls))]
public class AttackComponent : ControllableCharacterComponent
{
    public string AttackAnimKey = "Attack";

    public AttackSlot AttackArm;
    public AttackSlot AttackLeg;

    public AttackSlot RootArmAttack;
    public AttackSlot RootLegAttack;

    private GameObject maincamera;

    AttackSlot CurrentArmAttack;
    AttackSlot CurrentLegAttack;

    AttackSlot CurrentAttack;

    Animator animator;
    CharacterInput input;
    UnityInputCharacterControls controls;

    float timeout = 0.0f;
    const float AttackTimeout = 1.2f;

    public int _checkHealthComponentsInRange;

    List<HealthComponent> HealthComponentsInRange;

    ControllableCharacter character;

    EnergySystem kiraEnergy;

    InventoryController inventory;

    float AttackTimeMultiplier = 1.0f;

    Kira kira;

    private bool CanAttackNow()
    {
        SimpleClimb climb = GetComponent<SimpleClimb>();
        InteractComponent interact = GetComponent<InteractComponent>();
        if(climb != null && climb.GetClimbCollider() != null)
        {
            return false;
        }
        if(interact != null && interact.GetInteractor() != null)
        {
            return false;
        }
        return true;
    }

    protected void Start()
    {
        character = GetComponent<ControllableCharacter>();
        kira = GetComponent<Kira>();
        animator = GetComponent<Animator>();
        inventory = FindObjectOfType<InventoryController>();
        maincamera = GameObject.Find("Main Camera");

        kiraEnergy = GetComponent<EnergySystem>();
        
        ResetAttacks();

        HealthComponentsInRange = new List<HealthComponent>();

        controls = GetComponent<UnityInputCharacterControls>();

        this.input = new CharacterInput();
    }

    public override void ComponentInputUpdate(CharacterInput input)
    {
        this.input = input;
    }

    virtual protected AttackModifier CalculateModifiers()
    {
        InventoryController inventory = FindObjectOfType<InventoryController>();
        AttackModifier result = new AttackModifier();
        if(inventory != null)
        {
            result.HasBoots = inventory.HasBoots();
            result.HasGloves = inventory.HasGloves();
            result.HasStaff = inventory.HasItem(InventoryController.InventoryItems.Staff);
        }
        return result;
    }

    protected void ResetAttacks()
    {
       CurrentArmAttack = RootArmAttack;
       CurrentLegAttack = RootLegAttack;
    }

    protected void Update()
    {

        _checkHealthComponentsInRange = HealthComponentsInRange.Count;
        if(timeout > 0.0f)
        {
            timeout -= Time.deltaTime;
            if(timeout <= 0.0f)
            {
                ResetAttacks();
            }
        }

        if (character.IsBlocked) return;

        if (!CanAttackNow()) return;

        if (!(kira.CurrentComponent is MoveBehaviour)) return;

        if(input.HandAttack)
        {
            TryToPrimeStaff();
            Debug.Log("Executing hand attack");
            CurrentArmAttack = ExecuteAttack(CurrentArmAttack);
            if(CurrentArmAttack == null)
            {
                ResetAttacks();
            }
        }

        if (input.LegAttack)
        {
            Debug.Log("Executing leg attack");
            CurrentLegAttack = ExecuteAttack(CurrentLegAttack);
            if(CurrentLegAttack == null)
            {
                ResetAttacks();
            }
        }

    }

    private void TryToPrimeStaff()
    {
        if (inventory == null) return;

        if (inventory.HasItem(InventoryController.InventoryItems.Staff))
        {
            StaffController staffController = GetComponent<StaffController>();
            staffController.SpawnStaff();

            BoomerangController boomerangController = GetComponent<BoomerangController>();
            if(boomerangController!=null)
            {
                boomerangController.PutBack();
            }
        }
    }

    protected AttackSlot ExecuteAttack(AttackSlot slot)
    {
        if(slot != null)
        {
            if (Input.GetJoystickNames().Length == 0)
            {
                if (maincamera.transform.rotation.eulerAngles.y < 0)
                {
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, maincamera.transform.rotation.eulerAngles.y + 360f, this.transform.rotation.eulerAngles.z);
                }
                else
                {
                    this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, maincamera.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
                }
            }

            kiraEnergy.DecreaseEnergy(10);
            CurrentAttack = slot;
            controls.Enabled = false;
            timeout = AttackTimeout * AttackTimeMultiplier;
            animator.SetInteger(AttackAnimKey, slot.AnimationIndex);
            return CurrentAttack.NextSlot;
        }
        CurrentAttack = null;
        return null;
    }

    protected void FinishAttack()
    {
        animator.SetInteger(AttackAnimKey, -1);
        controls.Enabled = true;
    }

    protected void OnTriggerEnter(Collider other)
    {
        HealthComponent otherHealth = other.gameObject.GetComponent<HealthComponent>();
        if (otherHealth != null)
        {
            if (!HealthComponentsInRange.Contains(otherHealth))
            {
                HealthComponentsInRange.Add(otherHealth);
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        HealthComponent otherHealth = other.gameObject.GetComponent<HealthComponent>();
        if (otherHealth != null)
        {
            HealthComponentsInRange.Remove(otherHealth);
        }
    }

    protected void Hit()
    {
        float Range = 1.8f;
        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (inventory != null && inventory.HasItem(InventoryController.InventoryItems.Gloves))
        {
            Range *= 1.5f;
        }
        if (inventory != null && inventory.HasItem(InventoryController.InventoryItems.Staff))
        {
            Range *= 2.0f;
            AttackTimeMultiplier = 1.75f;
        }

        if(CurrentAttack == null)
        {
            return;
        }

        foreach(HealthComponent health in HealthComponentsInRange)
        {
            if (health != null)
            {
                Vector3 thisToAttacked = (health.gameObject.transform.position - this.transform.position);
                float dist = thisToAttacked.magnitude;
                float DotProduct = Vector3.Dot(thisToAttacked.normalized, this.transform.forward);
                if (health != null && ((dist < Range && DotProduct > 0.1f) || !health.NeedDistance))
                    health.TakeDamage(CurrentAttack.BaseDamage, CalculateModifiers(), this.gameObject);
            }
        }
        FilterComponents();
    }

    protected void FilterComponents()
    {
        List<HealthComponent> GoodOnes = new List<HealthComponent>();
        foreach(HealthComponent component in HealthComponentsInRange)
        {
            if(component != null)
            {
                GoodOnes.Add(component);
            }
        }
        HealthComponentsInRange.Clear();
        foreach(HealthComponent component in GoodOnes)
        {
            HealthComponentsInRange.Add(component);
        }
    }

    protected void Release()
    {
        Debug.Log("RELEASE");
        FinishAttack();
    }

}
