using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAction3 : GameplayAction
{
    public GameObject ObjectToEnable;
    public float increaseSpeedMultiplier = 4;

    public override Coroutine DoAction(GameObject source)
    {
        ObjectToEnable.SetActive(true);
        StartCoroutine(EnableLavaLights());
        return StartCoroutine(FinishImmediately());
    }

    public IEnumerator EnableLavaLights()
    {
        foreach (Transform child in ObjectToEnable.transform)
        {
            if (child.gameObject.GetComponent<Light>())
            {
                switch (child.gameObject.name)
                {
                    case "Directional LavaLight":
                        while (child.gameObject.GetComponent<Light>().intensity < 0.9f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                    case "Directional Light (1)":
                        while (child.gameObject.GetComponent<Light>().intensity < 0.5f)
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
                    case "Aura Point Light (1)":
                        while (child.gameObject.GetComponent<Light>().intensity < 0.5f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                    case "Spot Light":
                        while (child.gameObject.GetComponent<Light>().intensity < 1f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                    case "Directional Light (2)":
                        while (child.gameObject.GetComponent<Light>().intensity < 0.4f)
                        {
                            Light childLight = child.gameObject.GetComponent<Light>();
                            childLight.intensity += increaseSpeedMultiplier * Time.deltaTime;
                            yield return null;
                        }
                        break;
                }
            }
        }
    }
}
