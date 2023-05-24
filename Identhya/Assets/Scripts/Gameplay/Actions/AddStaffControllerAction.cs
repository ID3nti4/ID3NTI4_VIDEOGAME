using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddStaffControllerAction : GameplayAction
{
    public override Coroutine DoAction(GameObject source)
    {
        Kira kira = FindObjectOfType<Kira>();
        Animator animator = kira.GetComponentInChildren<Animator>();
        WeaponsController weaponsController = kira.GetComponent<WeaponsController>();
        kira.GetComponent<StaffController>().SpawnStaff();
        //kira.gameObject.AddComponent<StaffController>();
        //source.GetComponent<SphereCollider>().enabled = false;
        //source.GetComponent<ParentFromTransform>().ParentTransform = weaponsController.HandSocket;
        //source.GetComponent<ParentConstraint>().Enabled = true;
        return StartCoroutine(FinishImmediately());
    }
}
