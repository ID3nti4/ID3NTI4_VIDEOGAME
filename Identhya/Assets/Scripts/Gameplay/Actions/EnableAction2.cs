using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAction2 : GameplayAction
{
    public GameObject ObjectToEnable;
    public float increaseSpeedMultiplier = 4;

    public override Coroutine DoAction(GameObject source)
    {
        StartCoroutine(EnableCavernLights());
        return StartCoroutine(FinishImmediately());
    }

    public IEnumerator EnableCavernLights()
    {
        foreach (Transform child in ObjectToEnable.transform)
        {
            if (child.gameObject.GetComponent<Light>())
            {
                switch (child.gameObject.name)
                {
                    case "DirectionalLavaLight":
                        while (child.gameObject.GetComponent<Light>().intensity < 0.75f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                    case "Directional Light (1)":
                        while (child.gameObject.GetComponent<Light>().intensity < 0.75f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                    case "Aura Point Light":
                        while (child.gameObject.GetComponent<Light>().intensity < 1f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                    case "Aura Spot Light":
                        while (child.gameObject.GetComponent<Light>().intensity < 3.48f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                }
            }
            else if (!child.gameObject.GetComponent<Light>())
            {
                //child.gameObject.SetActive(true);
            }
        }
    }
}
