using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Kick, Punch, MeleeWeapon, RangedWeapon };

[System.Serializable]
public class AttackSlot : MonoBehaviour
{
    public string Name;
    public AttackType Type;
    public float BaseDamage;
    public Effect effect;
    public float EffectProbability;
    public int AnimationIndex = -1;
    public AnimationClip Clip;
    public AttackSlot NextSlot;
}

