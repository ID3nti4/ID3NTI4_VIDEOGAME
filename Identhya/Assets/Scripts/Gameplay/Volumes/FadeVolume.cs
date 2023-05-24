using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeVolume : MonoBehaviour
{
    public UIFader uiFader;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Fade!");
            uiFader.FadeToOpaque();
        }
    }
}
