using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableCharacter : MonoBehaviour
{
    [SerializeField] 
    public CharacterControls pressInput;
    [SerializeField] 
    public CharacterControls downInput;

    public bool IsBlocked = false;
}
