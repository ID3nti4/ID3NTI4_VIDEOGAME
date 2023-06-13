using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAction : GameplayAction
{
    public GameObject Prefab;
    public Transform SpawnTransform;
    public override Coroutine DoAction(GameObject source)
    {
        Debug.Log("empieza Element0");
        GameObject newGO = (GameObject)(Instantiate(Prefab));
        newGO.transform.position = SpawnTransform.position;
        newGO.transform.rotation = SpawnTransform.rotation;
        newGO.transform.localScale = SpawnTransform.localScale;
        Debug.Log("termina Element0");
        return StartCoroutine(FinishImmediately());
    }
}
