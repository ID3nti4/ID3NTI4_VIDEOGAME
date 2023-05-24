using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    public Interactor CurrentInteractor = null;

    [HideInInspector]
    public bool isBlocked = false;

    ControlsOneTimePressFilter OneTimePress;

    Kira kira;

    void Awake()
    {
        kira = GetComponent<Kira>();
        OneTimePress = GetComponent<ControlsOneTimePressFilter>();
    }

    public Interactor GetInteractor()
    {
        return CurrentInteractor;
    }

    public void SetInteractor(Interactor interactor)
    {
        CurrentInteractor = interactor;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBlocked)
        {
            return;
        }
        if(OneTimePress.GetInput().Interact && CurrentInteractor != null)
        {
            CurrentInteractor.OnObjectInteracted?.Invoke();
            CurrentInteractor.Interact();
        }
    }
}
