using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutreMove : MonoBehaviour
{
    Rigidbody rigidbody;
    public float MoveSpeed = 20.0f;
    public float RotateSpeed = 20.0f;
    public float JumpStrength = 10.0f;
   public bool jumping = false;
   public bool grounded = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumping)
        {
            MoveForward();
            Rotate();
        }
        if(Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
    }

    private void MoveForward()
    {
        rigidbody.velocity = this.transform.forward * MoveSpeed * Input.GetAxis("Vertical");
    }

    private void Rotate()
    {
        this.transform.Rotate(new Vector3(0, RotateSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0));
    }

    private void Jump()
    {
        jumping = true;
        rigidbody.velocity = rigidbody.velocity + Vector3.up * JumpStrength;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumping = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
