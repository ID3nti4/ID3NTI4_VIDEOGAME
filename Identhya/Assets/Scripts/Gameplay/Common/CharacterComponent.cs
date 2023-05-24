using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterComponent : ControllableCharacterComponent
{
    public abstract void OnComponentActivate();

    public abstract void OnComponentDeactivate();

    public abstract void ComponentUpdate(float DeltaTime);

    public abstract bool CanActivate();
}
