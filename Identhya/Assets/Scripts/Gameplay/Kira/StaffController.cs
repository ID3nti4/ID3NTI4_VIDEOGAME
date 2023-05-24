using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    public GameObject Prefab;
    public AnimatorOverrideController animationOverride;

    ParentConstraint constraint = null;

    public bool ForceSpawnStaff = false;

    WeaponsController weaponsController;

    RuntimeAnimatorController previousController = null;

    private void Start()
    {
        weaponsController = GetComponent<WeaponsController>();
        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (ForceSpawnStaff)
        {
            SpawnStaff();
        }
        else if (inventory != null)
        {
            if (inventory.HasItem(InventoryController.InventoryItems.Staff))
            {
                SpawnStaffInBack();
            }
        }
    }

    bool staffInHand = false;

    public bool StaffInHand()
    {
        return staffInHand;
    }

    public void SpawnStaff()
    {
        RegisterAnimationsOverride();
        if (constraint == null)
        {
            GameObject newGO = (GameObject)Instantiate(Prefab);
            newGO.transform.localScale = Vector3.one;
            constraint = newGO.GetComponent<ParentConstraint>();
        }
        if (constraint != null)
        {
            constraint.Enabled = true;
            ParentFromTransform[] pft = constraint.GetComponentsInChildren<ParentFromTransform>();
            if (pft.Length == 2)
            {
                pft[0].ParentTransform = weaponsController.StaffSocket;
                pft[1].ParentTransform = weaponsController.HandSocket;
            }
            if(pft.Length == 1)
            {
                pft[0].ParentTransform = weaponsController.HandSocket;
            }
        }
        staffInHand = true;
    }

    public void SpawnStaffInBack()
    {
        RegisterAnimationsOverride();
        if (constraint == null)
        {
            GameObject newGO = (GameObject)Instantiate(Prefab);
            newGO.transform.localScale = Vector3.one;
            constraint = newGO.GetComponent<ParentConstraint>();
        }
        if (constraint != null)
        {
            constraint.Enabled = true;
            ParentFromTransform[] pft = constraint.GetComponentsInChildren<ParentFromTransform>();
            if (pft.Length == 2)
            {
                pft[0].ParentTransform = weaponsController.StaffSocket;
                pft[1].ParentTransform = weaponsController.StaffSocket;
            }
            if (pft.Length == 1)
            {
                pft[0].ParentTransform = weaponsController.StaffSocket;
            }
        }
        staffInHand = false;
    }

    public void PutBack()
    {
        if (constraint != null)
        {
            constraint.Enabled = true;
            ParentFromTransform[] pft = constraint.GetComponentsInChildren<ParentFromTransform>();
            if (pft.Length == 2)
            {
                pft[0].ParentTransform = weaponsController.StaffSocket;
                pft[1].ParentTransform = weaponsController.StaffSocket;
            }
            if (pft.Length == 1)
            {
                pft[0].ParentTransform = weaponsController.StaffSocket;
            }
        }
        UnregisterAnimationsOverride();
        staffInHand = false;
    }

    private void UnregisterAnimationsOverride()
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (previousController != null)
        {
            anim.runtimeAnimatorController = previousController;
        }
    }

    private void RegisterAnimationsOverride()
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim.runtimeAnimatorController != previousController)
        {
            previousController = anim.runtimeAnimatorController;
        }
        anim.runtimeAnimatorController = animationOverride;
    }
}
