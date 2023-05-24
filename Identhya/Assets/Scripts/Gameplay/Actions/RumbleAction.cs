using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleAction : GameplayAction
{
    public float Strength = 0.1f;
    public float Interval = 0.15f;

    public override Coroutine DoAction(GameObject source)
    {
        return StartCoroutine(Rumble(Strength, Interval));
    }

    IEnumerator Rumble(float strength, float interval)
    {
        Vector3 pos = this.transform.position;
        float Elapsed = 0.0f;
        while(Elapsed < interval)
        {
            Vector3 offset = Random.insideUnitSphere * strength;
            this.transform.position = pos + offset;
            yield return new WaitForEndOfFrame();
            Elapsed += Time.deltaTime;
        }
    }
}
