using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHealthComponent : HealthComponent
{
    public override float CalculateDamage(float Damage, AttackModifier modifier)
    {
        if(modifier.HasGloves == false)
        {
            return 0.0f;
        }
        return 10.0f;
    }
}
