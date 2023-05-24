using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float Timeout = 3.0f;

    private void Start()
    {
        StartCoroutine(Delay(Timeout));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
