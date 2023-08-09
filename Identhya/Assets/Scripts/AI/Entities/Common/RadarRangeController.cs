using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DictEntry
{
    public string StateName;
    [ColorUsage(true, true)]
    public Color StateColor;
}

public class RadarRangeController : MonoBehaviour
{
    public DictEntry[] StateColor;
    public Dictionary<string, Color> StateColorDict;

    public GameObject[] ObjectWithMaterial;

    public StateMachine stateMachine_A = null;

    private void Start()
    {
        StateColorDict = new Dictionary<string, Color>();
        foreach(DictEntry entry in StateColor)
        {
            StateColorDict[entry.StateName] = entry.StateColor;
        }
        if(stateMachine_A == null)
        {
            stateMachine_A = GetComponent<StateMachine>();
            if (stateMachine_A != null)
            {
                stateMachine_A.OnStateChanged += OnStateChanged;
            }
        }
    }

    private void OnStateChanged(string StateName, string StateClass)
    {
        SetStateColor(StateClass);
    }

    public void SetStateColor(string state)
    {
        if (!StateColorDict.ContainsKey(state)) return;

        foreach(GameObject obj in ObjectWithMaterial)
        {
            obj.GetComponent<Renderer>().material.SetColor("_MainColor", StateColorDict[state]);
        }
/*
        if (gameObject.name.Contains("EnemyDrone") && state == "Hostile")
        {
            gameObject.transform.Find("Capsule (1)").GetComponent<PlayerEnergyConsumption>().redLight = true;
        }
        else if (state != "Hostile")
        {
            gameObject.transform.Find("Capsule (1)").GetComponent<PlayerEnergyConsumption>().redLight = false;
        }
*/
    }
}
