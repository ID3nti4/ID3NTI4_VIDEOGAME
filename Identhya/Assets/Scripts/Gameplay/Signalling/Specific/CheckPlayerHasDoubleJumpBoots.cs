using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerHasDoubleJumpBoots : ConditionalInterface
{
    public override bool checkCondition()
    {
        //return FindObjectOfType<KiraJump>().DoubleJumpEnabled;
        return false;
    }
}
