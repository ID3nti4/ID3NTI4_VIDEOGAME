using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public float Damage = 0.0f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            if(Damage > 0.0f)
            {
                other.gameObject.GetComponent<HealthComponent>().TakeDamage(Damage);
            }
            else
            {
                other.gameObject.GetComponent<StunComponent>().Stun();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {

        }
    }
}
