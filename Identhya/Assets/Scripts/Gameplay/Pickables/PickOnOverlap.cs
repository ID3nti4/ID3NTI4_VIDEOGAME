using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOnOverlap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0.08f);
        }
    }
}
