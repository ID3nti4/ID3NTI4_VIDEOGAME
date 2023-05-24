using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatedCharacterComponent : CharacterComponent
{
    protected Animator animator;
    new protected Rigidbody rigidbody;

    public delegate void OnComponentFinishedDelegate();

    public OnComponentFinishedDelegate OnComponentFinished;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
}
