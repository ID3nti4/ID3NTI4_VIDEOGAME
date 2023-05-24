using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteAction : MonoBehaviour
{
    public GameplayAction ActionToExecute;
    public float Delay = 0.1f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(Delay);
        yield return ActionToExecute.DoAction(this.gameObject);
    }

}
