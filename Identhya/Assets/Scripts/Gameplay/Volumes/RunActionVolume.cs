using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunActionVolume : MonoBehaviour
{
    public GameplayAction action;
    public bool JustOnce = false;
    bool done = false;

    int inside = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (inside == 0)
            {
                Debug.Log("In");
                if (JustOnce && done)
                {
                    return;
                }
                done = true;
                action.DoAction(this.gameObject);
            }
            ++inside;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            --inside;
            Debug.Log("Outt");
        }
    }

}
