using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AnimatedCharacterComponent
{
	public string JumpKey = "Jump";
	public string GroundedKey = "Grounded";
	public string DoubleJumpAnimKey = "DoubleJump";
	public string SpeedKey = "Speed";
	public float JumpHeight = 1.5f;
	public float JumpIntertialForce = 1f;
	public float JumpStrengthMultiplier = 1.0f;
	public float AirControl = 0.5f;
	public float doubleJumpEnergyCost = 15;
	float HorizontalMultiplier = 1.0f;
	public bool hasLanded = false;
	public EnergySystem energySystem;

	bool jumpStarted = false;
	bool doubleJumpStarted = false;

	Vector3 InitialVelocity;
	float JumpSpeed = 0.0f;

	public bool UseInventory = true;

	private Kira kira;

	float horizontal;
	float vertical;

	public bool Enabled = true;

	InventoryController inventory;

	private new void Awake()
	{
		base.Awake();
		kira = GetComponent<Kira>();
		inventory = FindObjectOfType<InventoryController>();
	}

	public void ForceJump(Vector3 InitialVelocity)
	{
		rigidbody.velocity = InitialVelocity;
		kira.ActivateOneComponent<Jump>();
	}

	public override bool CanActivate()
	{
		if (!Enabled) return false;
		return kira.GetActiveComponent() is MoveBehaviour;
	}

	public override void ComponentInputUpdate(CharacterInput input)
	{
		energySystem = GetComponent<EnergySystem>();
		if (input.Jump && CanActivate() && !jumpStarted)
		{
			kira.ActivateOneComponent<Jump>();
		}
		else if(input.Jump && jumpStarted && !doubleJumpStarted && inventory!=null && inventory.HasBoots() && energySystem.currentEnergy < doubleJumpEnergyCost)
		{
			animator.SetTrigger(DoubleJumpAnimKey);
			doubleJumpStarted = true;
			energySystem.DecreaseEnergy(doubleJumpEnergyCost);
			PerformJump();
		}
		horizontal = input.MovementHorizontal;
		vertical = input.MovementVertical;
	}

	public override void OnComponentActivate()
    {
		InitialVelocity = rigidbody.velocity;
		Debug.Log("InitialVeliocity: " + InitialVelocity);

		jumpStarted = true;
		doubleJumpStarted = false;

		animator.SetBool(JumpKey, true);

		if(UseInventory)
		{
			JumpStrengthMultiplier = inventory.HasBoots() ? 2.5f : 1.0f;
			doubleJumpStarted = !inventory.HasBoots();
		}

		PerformJump();
		
	}

	private void PerformJump()
	{
		kira.ForceUngrounded();
		animator.applyRootMotion = false;
		kira.KillFriction();
		RemoveVerticalVelocity();
		float velocity = 2f * Mathf.Abs(Physics.gravity.y) * JumpHeight * JumpStrengthMultiplier;
		velocity = Mathf.Sqrt(velocity);
		rigidbody.velocity = InitialVelocity + Vector3.up * velocity;
		JumpSpeed = velocity;
	}


    public override void OnComponentDeactivate()
    {
		jumpStarted = false;
		doubleJumpStarted = false;
		animator.applyRootMotion = true;
		animator.SetBool(GroundedKey, true);
		animator.SetBool(JumpKey, false);
		kira.SetDefaultFriction();
	}

    public override void ComponentUpdate(float DeltaTime)
    {
		JumpManagement();
    }

	void JumpManagement()
	{
		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward.y = 0.0f;
		Vector3 input = new Vector3(horizontal, 0, vertical).normalized;
		input = input - input*Mathf.Max(Vector3.Dot(forward, input), 0.0f);
		rigidbody.AddForce(forward * AirControl * vertical * Time.deltaTime + Helpers.GetFlatPerpendicular(forward) * AirControl * horizontal * Time.deltaTime, ForceMode.Acceleration);
		if (kira.IsGrounded())
		{
			animator.applyRootMotion = true;
			animator.SetBool(GroundedKey, true);
			kira.SetDefaultFriction();
			animator.SetBool(JumpKey, false);
			kira.ActivateMovement(true);
		}
		
	}

	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = rigidbody.velocity;
		horizontalVelocity.y = 0;
		rigidbody.velocity = horizontalVelocity;
	}

	private IEnumerator Landing()
    {
		hasLanded = true;
		yield return new WaitForSeconds(.1f);
		hasLanded = false;
		yield return null;
    }
}
