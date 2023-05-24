using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectAction : GameplayAction
{
    public GameObject Prefab;
    public Transform SpawnTransform;
    public override Coroutine DoAction(GameObject source)
    {
        GameObject newGO = (GameObject)(Instantiate(Prefab));
        newGO.transform.position = SpawnTransform.position;
        newGO.transform.rotation = SpawnTransform.rotation;
        newGO.transform.localScale = SpawnTransform.localScale;
        return StartCoroutine(FinishImmediately());
    }
}
