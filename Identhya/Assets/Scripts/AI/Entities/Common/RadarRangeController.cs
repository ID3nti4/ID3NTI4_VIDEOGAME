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

    public HealthComponent kiraHealth;

    public bool fromHostile;

    private void Start()
    {
        StateColorDict = new Dictionary<string, Color>();

        kiraHealth = GameObject.Find("Kira_V2").GetComponent<HealthComponent>();

        foreach (DictEntry entry in StateColor)
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
        Debug.Log("se cambia al estado " + StateName);
    }

    public void SetStateColor(string state)
    {
        if (!StateColorDict.ContainsKey(state)) return;

        foreach(GameObject obj in ObjectWithMaterial)
        {
            obj.GetComponent<Renderer>().material.SetColor("_MainColor", StateColorDict[state]);
        }

        if (gameObject.name.Contains("EnemyDrone") && state == "Hostile")
        {
            Debug.Log("Hostile State Detected"); 
            kiraHealth.IncreaseRedLightCounter(1f);
            kiraHealth.StartCoroutine("GradualHealthDecrease");
            fromHostile = true;
        }
        else if (gameObject.name.Contains("EnemyDrone") && state != "Hostile" && fromHostile == true)
        {
            kiraHealth.DecreaseRedLightCounter(1f);
        }

    }
}
