using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneState_Patrol : DroneState
{
    public Transform[] WayPoints;
    Sight sight;
    DroneAIFlyTo flyToController;
    [SerializeField] private int nextWaypoint;
                                  
    public float journeyLength;
    public float gottaGoFast;
    float fractionOfJourney;

    [SerializeField] AudioClip patrolSound;

    public float PerceptionDisabledTime = 1.0f;

    new void Awake()
    {
        base.Awake();
        fractionOfJourney = 0;
        nextWaypoint = 0;
        flyToController = GetComponent<DroneAIFlyTo>();
        sight = GetComponent<Sight>();
    }

    public override void OnStateEnter(State prevState)
    {
        GetComponent<Sight>().OnObjectSighted += OnTargetSighted;
        GetComponent<DroneFXController>().Play(patrolSound, true);
        RefreshDestinationWayPoint();
        sight.SetVisionAngle(Drone.MiddleVisionAngle);
        flyToController.stopAtTarget = false;
        
    }

    public override void OnStateLeave(State nextState)
    {
        GetComponent<Sight>().OnObjectSighted -= OnTargetSighted;
    }

    public override void UpdateState()
    {
        if (flyToController.IsDone())
        {
            RefreshDestinationWayPoint();
        }

        if (PerceptionDisabledTime > 0.0f)
        {
            PerceptionDisabledTime -= Time.deltaTime;
            return; // do not check for player...
        }

    }

    private void OnTargetSighted(GameObject obj, GameObject sighter)
    {
        StartCoroutine(OnTargetSighterCoRo());
    }

    IEnumerator OnTargetSighterCoRo()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("<color=yellow> Player SPOTTED!! </color>");
        machine.SetState(GetComponent<DroneState_Chasing>());
    }

    void RefreshDestinationWayPoint()
    {
        if (WayPoints.Length == 0)
            return;

        nextWaypoint = (nextWaypoint + 1) % WayPoints.Length;
        flyToController.FlyTo(WayPoints[nextWaypoint]);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            machine.SetState<DroneState>(GetComponent<DroneState_Chasing>());
        }
    }
}
