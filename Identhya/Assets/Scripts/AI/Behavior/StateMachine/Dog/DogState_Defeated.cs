using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogState_Defeated : DogState
{
    [SerializeField] string DefeatAnimKey = "Dead";
    [SerializeField] AudioClip audioClip;

    public GameObject DeathFX;

    public override void OnStateEnter(State prevState)
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<NavMeshAgent>().isStopped = true;
        dog.animator.SetBool(DefeatAnimKey, true);
        dog.rigidbody.velocity = Vector3.zero;
        dog.rigidbody.angularVelocity = Vector3.zero;
        dog.rigidbody.isKinematic = true;
        dog.GetComponent<SphereCollider>().enabled = false;
        dog.GetComponent<Sight>().Enabled = false;
        dog.GetComponent<NavMeshAgent>().enabled = false;
        StartCoroutine(BuryDog(0.3f));
    }

    IEnumerator BuryDog(float DescentSpeed)
    {
        dog.animator.SetBool(DefeatAnimKey, true);
        yield return new WaitForSeconds(5.0f);

        Vector3 InitialPosition = dog.transform.position;
        float Descent = 0.0f;

        /*while (Descent < 3.0f)
        {
            Vector3 NewPosition = InitialPosition + Vector3.down * Descent;
            Descent += DescentSpeed * Time.deltaTime;
            dog.transform.position = NewPosition;
            yield return new WaitForEndOfFrame();
        }*/
        FindObjectOfType<SFXController>().PlaySound(audioClip);
        ((GameObject)Instantiate(DeathFX)).transform.position = InitialPosition;

        Destroy(dog.gameObject);
    }
}
