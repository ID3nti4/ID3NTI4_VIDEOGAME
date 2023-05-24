using UnityEngine;

// MoveBehaviour inherits from GenericBehaviour. This class corresponds to basic walk and run behaviour, it is the default behaviour.
public class MoveBehaviour : GenericBehaviour
{
	public float walkSpeed = 0.15f;                 // Default walk speed.
	public float runSpeed = 1.0f;                   // Default run speed.
	public float sprintSpeed = 2.0f;                // Default sprint speed.
	public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.
	public string jumpButton = "Jump";              // Default jump button.
	public float jumpHeight = 1.5f;                 // Default jump height.
	public float jumpIntertialForce = 10f;          // Default horizontal inertial force when jumping.

	private float speed, speedSeeker;               // Moving speed.
	private int jumpBool;                           // Animator variable related to jumping.
	private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
	private bool jump;                              // Boolean to determine whether or not the player started a jump.
	private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.
	private bool isRunningParticles = false;
	private bool CanMove = true;

	public bool _Enabled = true;

	CharacterInput input;
	UnityInputCharacterControls controls;
	private ParticleSystem runParticles;

	// Start is always called after any Awake functions.
	void Start()
	{
		// Set up the references.
		jumpBool = Animator.StringToHash("Jump");
		groundedBool = Animator.StringToHash("Grounded");
		behaviourManager.GetAnim.SetBool(groundedBool, true);

		// Subscribe and register this behaviour as the default behaviour.
		behaviourManager.SubscribeBehaviour(this);
		behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
		speedSeeker = runSpeed;

		controls = GetComponent<UnityInputCharacterControls>();
		runParticles = GameObject.Find("RunParticlesSystem").GetComponent<ParticleSystem>();
	}

	public override void ComponentInputUpdate(CharacterInput input)
	{
	
	}

	public override bool CanActivate()
	{
		return true;
	}

	public override void OnComponentActivate()
	{
		
	}

	public override void OnComponentDeactivate()
	{
		CanMove = false;
	}

	public override void ComponentUpdate(float DeltaTime)
	{
		MovementManagement(behaviourManager.GetH, behaviourManager.GetV);
	}

	public void SetIsRunning(bool running)
	{

	}


	// Deal with the basic player movement
	void MovementManagement(float horizontal, float vertical)
	{
		if (!controls.Enabled) return;
		if (!_Enabled) return;
		if (!CanMove) return;
		// On ground, obey gravity.
		if (behaviourManager.IsGrounded())
			behaviourManager.GetRigidBody.useGravity = true;

		// Avoid takeoff when reached a slope end.
		else if (!behaviourManager.GetAnim.GetBool(jumpBool) && behaviourManager.GetRigidBody.velocity.y > 0)
		{
			RemoveVerticalVelocity();
		}

		Rotating(horizontal, vertical);

		// Set proper speed.
		Vector2 dir = new Vector2(horizontal, vertical);
		speed = Vector2.ClampMagnitude(dir, 1f).magnitude;
		// This is for PC only, gamepads control speed via analog stick.
		speedSeeker += Input.GetAxis("Mouse ScrollWheel");
		speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
		speed *= speedSeeker;
		if (behaviourManager.IsSprinting())
		{
            if (!isRunningParticles)
            {
				runParticles.Play();
				isRunningParticles = true;
			}
			speed = sprintSpeed;
        }
		else
		{
			runParticles.Stop();
			isRunningParticles = false;
		}

		behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);
	}

	private void RemoveVerticalVelocity()
	{
		Vector3 horizontalVelocity = behaviourManager.GetRigidBody.velocity;
		horizontalVelocity.y = 0;
		behaviourManager.GetRigidBody.velocity = horizontalVelocity;
	}

	Vector3 Rotating(float horizontal, float vertical)
	{
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);

		forward.y = 0.0f;
		forward = forward.normalized;

		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		Vector3 targetDirection;
		targetDirection = forward * vertical + right * horizontal;

		if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero))
		{
			Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

			Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
			behaviourManager.GetRigidBody.MoveRotation(newRotation);
			behaviourManager.SetLastDirection(targetDirection);
		}
		// If idle, Ignore current camera facing and consider last moving direction.
		if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9))
		{
			behaviourManager.Repositioning();
		}

		return targetDirection;
	}

	public void SetCanMove()
	{
		this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
		GetComponent<Jump>().Enabled = true; // @TODO : Create a higher level logic to manage intercomponent interactions; Move should not know anything about Jump
		GetComponent<SimpleClimb>().Enabled = true; // @TODO : Create a higher level logic to manage intercomponent interactions; Move should not know anything about Jump
		rigidbody.useGravity = true;
		Debug.Log("Can move!!");
		CanMove = true;
	}
	
}
