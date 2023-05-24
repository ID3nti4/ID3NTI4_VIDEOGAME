using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogState_Loitering : DogState
{
    NavMeshAgent agent;

    public GameObject debug_pos_N;

    [SerializeField] int MinMoves = 1;
    [SerializeField] int MaxMoves = 4;
    [SerializeField] float MinDelay = 1.0f;
    [SerializeField] float MaxDelay = 3.0f;

    [SerializeField] float moveMaxAngle = 35.0f;
    [SerializeField] float medMaxAngle = 95.0f;
    [SerializeField] float largeMaxAngle = 190.0f;

    [SerializeField] float MedTurnProb = 0.2f;

    [SerializeField] float LargeTurnProb = 0.1f;

    [SerializeField] string LookAroundAnimKey = "Look Around";
    [SerializeField] string SniffAnimKey = "Eating";

    Coroutine loiter;

    public override void OnStateEnter(State prevState)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = dog.WalkSpeed;
        loiter = StartCoroutine(Loiter());
        agent.speed = dog.WalkSpeed;
        agent.isStopped = false;
        GetComponent<Sight>().OnObjectSighted += OnTargetSighted;
        GetComponent<Hearing>().OnObjectHeard += OnTargetHeard;
        GetComponent<Sonar>().OnObstacleSensed += OnObstacleInPath;
    }

    private void OnTargetSighted(GameObject obj, GameObject sighter)
    {
        //Debug.Log("Switching to ENGAGING");
        machine.SetState(GetComponent<DogState_Engaging>());
    }

    private void OnTargetHeard(GameObject obj, GameObject hearer)
    {
        GetComponent<DogState_Investigating>().soundLocation = obj.transform.position;
        machine.SetState(GetComponent<DogState_Investigating>());
    }

    public override void OnStateLeave(State nextState)
    {
        GetComponent<Sonar>().OnObstacleSensed -= OnObstacleInPath;
        GetComponent<Sight>().OnObjectSighted -= OnTargetSighted;
        GetComponent<Hearing>().OnObjectHeard -= OnTargetHeard;
        dog.animator.SetBool(LookAroundAnimKey, false);
        dog.animator.SetBool(SniffAnimKey, false);
        StopCoroutine(loiter);
    }

    private void OnObstacleInPath(GameObject obstacle)
    {
        StopCoroutine(loiter);
        dog.animator.SetBool(LookAroundAnimKey, false);
        dog.animator.SetBool(SniffAnimKey, false);
        StartCoroutine(AvoidObstacle());
    }

    IEnumerator AvoidObstacle()
    {
        Debug.Log(dog.name + "   ====>   Avoiding Obstacle!! ");
        Vector3 Destination = Utils.FindNavigableLocation(dog.transform.position, dog.transform.forward, 1.0f, 3.0f, 160.0f, 180.0f);
        yield return StartCoroutine(MoveTo(Destination));
        float TimeToWait = Random.Range(MinDelay / 2.0f, MaxDelay / 2.0f);
        yield return new WaitForSeconds(TimeToWait);
        loiter = StartCoroutine(Loiter());
    }

    IEnumerator Loiter()
    {
        for(; ; )
        {
            agent.speed = Utils.TryProbability(0.5f) ? dog.WalkSpeed : dog.SearchSpeed;
            int Moves = Random.Range(MinMoves, MaxMoves + 1);
            for (int i = 0; i < Moves; ++i)
            {
                Vector3 Destination = Utils.FindNavigableLocation(dog.transform.position, dog.transform.forward, 3.0f, 8.0f, 0.0f, moveMaxAngle);
                if(debug_pos_N != null)
                {
                    debug_pos_N.transform.position = Destination;
                }
                yield return StartCoroutine(MoveTo(Destination));
            }

            string key = Utils.TryProbability(0.5f) ? LookAroundAnimKey : SniffAnimKey;
            dog.animator.SetBool(key, true);
            float TimeToWait = Random.Range(MinDelay, MaxDelay);
            yield return new WaitForSeconds(TimeToWait);
            dog.animator.SetBool(key, false);

            if (Utils.TryProbability(MedTurnProb))
            {
                //Debug.Log("<color=magenta>" + dog.name + "   ====>   Medium turn </magenta>");
                Vector3 Destination = Utils.FindNavigableLocation(dog.transform.position, dog.transform.forward, 1.0f, 3.0f, moveMaxAngle, medMaxAngle);
                yield return StartCoroutine(MoveTo(Destination));
                TimeToWait = Random.Range(MinDelay/2.0f, MaxDelay/2.0f);
                yield return new WaitForSeconds(TimeToWait);
            }
            else if (Utils.TryProbability(LargeTurnProb))
            {
                //Debug.Log("<color=cyan>" + dog.name + "   ====>   Large turn </color>");
                Vector3 Destination = Utils.FindNavigableLocation(dog.transform.position, dog.transform.forward, 1.0f, 3.0f, medMaxAngle, largeMaxAngle);
                yield return StartCoroutine(MoveTo(Destination));
                TimeToWait = Random.Range(MinDelay/2.0f, MaxDelay/2.0f);
                yield return new WaitForSeconds(TimeToWait);
            }

        }
    }

    IEnumerator MoveTo(Vector3 Destination)
    {
        agent.destination = Destination;
        while (agent.remainingDistance > 0.1f)
        {
            yield return new WaitForSeconds(0.15f);
        }
    }
}
