using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsOneTimePressFilter : CharacterControls
{
    public CharacterControls sourceControls;
    public CharacterInput prevInput = new CharacterInput();

    public override CharacterInput GetInput()
    {
        return currentInput;
    }

    private void Update()
    {
        CharacterInput sourceInput = sourceControls.GetInput();
        FilterInput(ref sourceInput.Dash, ref prevInput.Dash, ref currentInput.Dash);
        FilterInput(ref sourceInput.Interact, ref prevInput.Interact, ref currentInput.Interact);
        FilterInput(ref sourceInput.HandAttack, ref prevInput.HandAttack, ref currentInput.HandAttack);
        FilterInput(ref sourceInput.LegAttack, ref prevInput.LegAttack, ref currentInput.LegAttack);
        FilterInput(ref sourceInput.Jump, ref prevInput.Jump, ref currentInput.Jump);
        FilterInput(ref sourceInput.Extra, ref prevInput.Extra, ref currentInput.Extra);
        currentInput.MovementHorizontal = sourceInput.MovementHorizontal;
        currentInput.MovementVertical = sourceInput.MovementVertical;
    }

    private void FilterInput(ref bool source, ref bool prev, ref bool output)
    {
        if (source)
        {
            if (prev)
            {
                output = false;
            }
            else
            {
                prev = output = true;
            }
        }
        else
        {
            prev = false;
            output = false;
        }
    }

}
