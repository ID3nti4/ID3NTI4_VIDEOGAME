using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimBehaviourBasic))]
[RequireComponent(typeof(WeaponsController))]
public class BoomerangController : MonoBehaviour
{
    public GameObject Prefab;
    public string ShootAnimKey = "Shoot";

    public GameObject StartPointProbe_N;
    public GameObject TargetPointProbe_N;

    public float ThrowLateralSpeedFraction = 0.65f;
    public float Speed = 2.0f;

    Boomerang boomerang;

    Animator animator;

    ParentSourceBlender parentBlender;
    ParentConstraint constraint;

    WeaponsController weaponsController;

    public bool ForceSpawnBoomerang = false;

    int BoomerangsInPossession = 0;

    private void Start()
    {
        weaponsController = GetComponent<WeaponsController>();

        BoomerangsInPossession = 0;

        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (ForceSpawnBoomerang)
        {
            SpawnBoomerang();
        }
        else if (inventory != null)
        {
            if(inventory.HasItem(InventoryController.InventoryItems.Boomerang))
            {
                SpawnBoomerang();                
            }
        }

        animator = GetComponentInChildren<Animator>();

        AimBehaviourBasic aim = GetComponent<AimBehaviourBasic>();
        if(aim != null)
        {
            aim.OnAimStart += PrimeBoomerang;
            aim.OnAimShoot += TryToShoot;
        }
        
    }

    private void PrimeBoomerang()
    {
        if(parentBlender != null)
        {
            parentBlender.Blend = 1.0f;
        }
    }

    private void TryToShoot()
    {
        if(BoomerangsInPossession > 0)
        {
            animator.SetTrigger(ShootAnimKey);
            constraint.Enabled = false;
            if (StartPointProbe_N != null)
            {
                StartPointProbe_N.transform.position = weaponsController.HandSocket.transform.position;
            }
            if (TargetPointProbe_N != null)
            {
                TargetPointProbe_N.transform.position = CalculateTargetPoint();
            }
            boomerang.Launch(weaponsController.HandSocket.transform.position, CalculateTargetPoint(), CalculateStartingVelocity(), CalculateTargetVelocity(), CalculateReturnVelocity(), weaponsController.HandSocket);
            BoomerangsInPossession--;
            boomerang.GetComponentInChildren<Rotate>().Go();
            
        }
        else
        {
            FindObjectOfType<AimBehaviourBasic>().isShooting = false;
        }
    }

    private void RecoverBoomerang()
    {
        constraint.Enabled = true;
        FindObjectOfType<AimBehaviourBasic>().isShooting = false;
        boomerang.GetComponentInChildren<Rotate>().Reset();
        ++BoomerangsInPossession;
    }

    Vector3 CalculateStartingVelocity()
    {
        Vector3 ThisToTarget = CalculateTargetPoint() - weaponsController.HandSocket.position;
        Quaternion rot = Quaternion.Euler(0, -ThrowLateralSpeedFraction, 0);
        return rot * (ThisToTarget.normalized * boomerang.Speed);
    }

    Vector3 CalculateTargetVelocity()
    {
        Vector3 ThisToTarget = CalculateTargetPoint() - weaponsController.HandSocket.position;
        Vector3 TargetVel = Vector3.Cross(ThisToTarget.normalized, Vector3.up) * boomerang.Speed;
        return TargetVel;
    }

    Vector3 CalculateReturnVelocity()
    {
        Vector3 ThisToTarget = CalculateTargetPoint() - weaponsController.HandSocket.position;
        Quaternion rot = Quaternion.Euler(0, ThrowLateralSpeedFraction, 0);
        return rot * (ThisToTarget.normalized * boomerang.Speed);
    }

    Vector3 CalculateTargetPoint()
    {
        RaycastHit Hit;
        
        if(Physics.Raycast(weaponsController.HandSocket.transform.position, FindObjectOfType<ThirdPersonOrbitCamBasic>().transform.forward, out Hit, 18.0f) && Hit.collider.gameObject.tag == "Enemy")
        {
            Debug.DrawLine(weaponsController.HandSocket.transform.position, weaponsController.HandSocket.transform.position + FindObjectOfType<ThirdPersonOrbitCamBasic>().transform.forward * 18.0f, Color.green);
            return Hit.collider.gameObject.transform.position;
        }
        else
        {
            Debug.DrawLine(weaponsController.HandSocket.transform.position, weaponsController.HandSocket.transform.position + FindObjectOfType<ThirdPersonOrbitCamBasic>().transform.forward * 18.0f, Color.red);
            return weaponsController.HandSocket.transform.position + FindObjectOfType<ThirdPersonOrbitCamBasic>().transform.forward * 10.0f;
        }
        
    }

    public void PutBack()
    {
        if (parentBlender != null)
        {
            parentBlender.Blend = 0.0f;
        }
    }

    public void SpawnBoomerang()
    {
        GameObject newGO = (GameObject)Instantiate(Prefab);
        newGO.transform.localScale = Vector3.one;
        constraint = newGO.GetComponent<ParentConstraint>();
        if(constraint != null)
        {
            constraint.Enabled = true;
            ParentFromTransform[] pft = constraint.GetComponentsInChildren<ParentFromTransform>();
            if(pft.Length == 2)
            {
                pft[0].ParentTransform = weaponsController.BackSocket;
                pft[1].ParentTransform = weaponsController.HandSocket;
            }
        }
        parentBlender = newGO.GetComponentInChildren<ParentSourceBlender>();
        boomerang = newGO.GetComponent<Boomerang>();
        boomerang.Speed = Speed;
        boomerang.ThrowLateralSpeedFraction = ThrowLateralSpeedFraction;
        boomerang.OnBoomerangeReturn += RecoverBoomerang;
        BoomerangsInPossession++;
    }
}
