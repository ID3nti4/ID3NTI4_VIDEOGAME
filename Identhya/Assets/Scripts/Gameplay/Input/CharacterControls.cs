using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInput
{
    public float MovementHorizontal;
    public float MovementVertical;
    public float CameraOrbitHorizontal;
    public float CameraOrbitVertical;
    public bool Dash;
    public bool Jump;
    public bool HandAttack;
    public bool LegAttack;
    public bool Interact;
    public bool Extra;
}

public abstract class CharacterControls : MonoBehaviour
{
    [SerializeField]
    protected CharacterInput currentInput = new CharacterInput();
    public abstract CharacterInput GetInput();
}
