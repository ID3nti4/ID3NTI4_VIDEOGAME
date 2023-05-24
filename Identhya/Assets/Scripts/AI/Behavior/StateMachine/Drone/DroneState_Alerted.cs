using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneState_Alerted : DroneState
{
    //public float velocidadGiroBusqueda = 120f;
    public float SeekDuration = 7f;
    Sight sight;
    DroneAIFlyTo DroneFlyTo;
    private float seekTime;
    public AudioClip alertSound;

    void Awake()
    {
        sight = GetComponent<Sight>();
        DroneFlyTo = GetComponent<DroneAIFlyTo>();
    }

    public override void OnStateEnter(State prevState)
    {
        sight.OnObjectSighted += OnTargetSighted;
        GetComponent<DroneFXController>().Play(alertSound, true);
        seekTime = 0f;
        sight.SetVisionAngle(Drone.WideVisionAngle);
        DroneFlyTo.StopMovement();
    }

    public override void OnStateLeave(State nextState)
    {
        sight.OnObjectSighted -= OnTargetSighted;
    }
    
    private void OnTargetSighted(GameObject obj, GameObject sighter)
    {
        Debug.Log("<color=yellow> Player SPOTTED!! </color>");
        machine.SetState<DroneState>(GetComponent<DroneState_Chasing>());
    }

    public override void UpdateState()
    {
        seekTime += Time.deltaTime;

        if (seekTime >= SeekDuration)
        {
            //navMeshAgent.speed = 2.5f; //AQUI INDICAMOS QUE LA VELOCIDAD TRAS LA HUIDA VUELVA A SER LA ASOCIADA CON PATRULLAR
            if (machine != null)
            {
                DroneState_Patrol p = GetComponentInChildren<DroneState_Patrol>();
                if (p != null)
                {
                    machine.SetState(p);
                }
                else
                {
                    Debug.Log("Dronestate_patrol is null");
                }
            }
            else
            {
                Debug.Log("Machine is null");
                machine = GetComponentInChildren<StateMachine>();
            }
        }
    }
}
