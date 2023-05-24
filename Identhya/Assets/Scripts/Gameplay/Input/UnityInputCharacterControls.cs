using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityInputCharacterControls : CharacterControls
{
    public string InteractButtonName;
    public string JumpButtonName;
    public string DashButtonName;
    public string ExtraButtonName;
    public string HandAttackName;
    public string LegAttackName;
    public string MoveHorizontalAxisName;
    public string MoveVerticalAxisName;
    public string CameraHorizontalAxisName;
    public string CameraVerticalAxisName;

    public bool Enabled = true;

    CharacterInput nullInput = new CharacterInput();

    public override CharacterInput GetInput()
    {
        if(!Enabled)
        {
            currentInput.Dash = false;
            currentInput.Jump = false;
            currentInput.Interact = false;
            currentInput.Extra = false;
            currentInput.HandAttack = false;
            currentInput.LegAttack = false;
            currentInput.MovementHorizontal = 0.0f;
            currentInput.MovementVertical = 0.0f;
            return currentInput;
        }
        currentInput.Dash = Input.GetButton(DashButtonName);
        currentInput.Jump = Input.GetButton(JumpButtonName);
        currentInput.Interact = Input.GetButton(InteractButtonName);
        currentInput.Extra = Input.GetButton(ExtraButtonName);
        currentInput.HandAttack = Input.GetButton(HandAttackName);
        currentInput.LegAttack = Input.GetButton(LegAttackName);
        currentInput.MovementHorizontal = Input.GetAxis("Horizontal");
        currentInput.MovementVertical = Input.GetAxis("Vertical");
        return currentInput;
    }
}
