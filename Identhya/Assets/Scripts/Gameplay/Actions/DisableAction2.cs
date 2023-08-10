﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAction2 : GameplayAction
{

    public GameObject ObjectToDisable;
    public float decreaseSpeedMultiplier = 2f;

    public override Coroutine DoAction(GameObject source)
    {
        StartCoroutine(DisableCavernLights());
        return StartCoroutine(FinishImmediately());
    }

    public IEnumerator DisableCavernLights()
    {
        foreach(Transform child in ObjectToDisable.transform)
        {
            if (child.gameObject.GetComponent<Light>())
            {
                while(child.gameObject.GetComponent<Light>().intensity > 0)
                {
                    Debug.Log("Entra al while");
                    Light childLight = child.gameObject.GetComponent<Light>();
                    childLight.intensity -= decreaseSpeedMultiplier * Time.deltaTime;
                    Debug.Log(child.gameObject.name + "intensidad: " + childLight.intensity);
                    yield return null;
                }
            }
            else if (!child.gameObject.GetComponent<Light>())
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    
}
