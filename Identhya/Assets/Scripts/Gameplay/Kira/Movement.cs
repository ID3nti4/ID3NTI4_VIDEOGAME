using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : AnimatedCharacterComponent
{
    bool grounded = true;
    bool canMove = true;

    public float MoveSpeed = 20.0f;
    public float RotateSpeed = 20.0f;

    float horizontal, vertical;

    public override void ComponentInputUpdate(CharacterInput input)
    {
        if (canMove)
        {
            horizontal = input.MovementHorizontal;
            vertical = input.MovementVertical;
        }
        else
        {
            horizontal = vertical = 0.0f;
        }
    }

    public override void ComponentUpdate(float DeltaTime)
    {
        if (!canMove) return;
   
        float ySpeed = rigidbody.velocity.y;
        Vector3 newVelocity = this.transform.forward * MoveSpeed * vertical;
        newVelocity.y = ySpeed;
        rigidbody.velocity = newVelocity;
    }

    public override bool CanActivate()
    {
        return true;
    }

    public override void OnComponentActivate()
    {
        canMove = true;
    }

    public override void OnComponentDeactivate()
    {
        canMove = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    public void SetCanMove()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        rigidbody.useGravity = true;
        Debug.Log("Can move!!");
        canMove = true;
    }
}

