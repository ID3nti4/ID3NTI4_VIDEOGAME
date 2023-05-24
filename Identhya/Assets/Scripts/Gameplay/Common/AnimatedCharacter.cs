using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    public Animator animator;
    public new Rigidbody rigidbody;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponentInChildren<Rigidbody>();
    }
}
