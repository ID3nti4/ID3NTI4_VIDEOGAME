using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControllableCharacterComponent : MonoBehaviour
{
    public abstract void ComponentInputUpdate(CharacterInput input);
}
