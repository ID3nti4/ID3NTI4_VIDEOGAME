using UnityEngine;
using System.Collections;

// AimBehaviour inherits from GenericBehaviour. This class corresponds to aim and strafe behaviour.
public class AimBehaviourBasic : GenericBehaviour
{

	public delegate void AimEventDelegate();
	public AimEventDelegate OnAimStart;
	public AimEventDelegate OnAimEnded;
	public AimEventDelegate OnAimShoot;

	public string aimButton = "Aim", shoulderButton = "Aim Shoulder";     // Default aim and switch shoulders buttons.
	public string AimIdleAnimKey = "Aim";
	public Texture2D crosshair;                                           // Crosshair texture.
	public float aimTurnSmoothing = 0.15f;                                // Speed of turn response when aiming to match camera facing.
	public Vector3 aimPivotOffset = new Vector3(0.5f, 1.2f,  0f);         // Offset to repoint the camera when aiming.
	public Vector3 aimCamOffset   = new Vector3(0f, 0.2f, -0.7f);         // Offset to relocate the camera when aiming.

	private int aimBool;                                                  // Animator variable related to aiming.
	private bool aim;                                                     // Boolean to determine whether or not the player is aiming.

	private Kira kira;

	public bool fire;
	public bool isShooting = false;

	private bool CanAimNow()
	{
		InventoryController inventory = FindObjectOfType<InventoryController>();
		if(inventory != null)
		{
			if(!inventory.HasItem(InventoryController.InventoryItems.Boomerang))
			{
				return false;
			}
		}

		SimpleClimb climb = GetComponent<SimpleClimb>();
		if(climb != null && climb.GetClimbCollider() != null)
		{
			return false;
		}
		return true;
	}

	new protected void Awake()
	{
		base.Awake();
		kira = GetComponent<Kira>();
	}

	// Start is always called after any Awake functions.
	void Start ()
	{
		// Set up the references.
		aimBool = Animator.StringToHash("Aim");
	}

	public override void ComponentInputUpdate(CharacterInput input)
	{
		if((input.Interact || input.HandAttack) && aim)
		fire = true;
		
		if (((Input.GetAxisRaw(aimButton) != 0) || Input.GetKey(KeyCode.Q)) && !aim && CanActivate() && CanAimNow())
		{
			Debug.Log(" ###############################################  ");
			kira.ActivateOneComponent<AimBehaviourBasic>();
		}
		else if (!isShooting && aim && (Input.GetAxisRaw(aimButton) == 0 && !Input.GetKey(KeyCode.Q)))
		{
			StartCoroutine(ToggleAimOff());
		}
	}

	public override bool CanActivate()
	{
		return kira.GetActiveComponent() is MoveBehaviour;
	}

	public override void OnComponentActivate()
	{
		animator.SetBool(AimIdleAnimKey, true);
		OnAimStart?.Invoke();
		StartCoroutine(ToggleAimOn());
		StaffController staffController = GetComponent<StaffController>();
		if (staffController != null)
		{
			staffController.PutBack();
		}
	}

	public override void OnComponentDeactivate()
	{
		animator.SetBool(AimIdleAnimKey, false);
		OnAimEnded?.Invoke();
	}

	public override void ComponentUpdate(float DeltaTime)
	{

		if (aim)
			behaviourManager.GetCamScript.SetTargetOffsets(aimPivotOffset, aimCamOffset);

		Rotating();

		canSprint = !aim;

		if (aim && Input.GetButtonDown(shoulderButton))
		{
			aimCamOffset.x = aimCamOffset.x * (-1);
			aimPivotOffset.x = aimPivotOffset.x * (-1);
		}

		if(aim && fire && !isShooting)
		{
			Debug.Log("<color=blue> ==================== AIM AND FIRE!!! ======================= </color>");
			isShooting = true;
			fire = false;
			OnAimShoot?.Invoke();
		}


	}


	// Co-rountine to start aiming mode with delay.
	private IEnumerator ToggleAimOn()
	{
		yield return new WaitForSeconds(0.05f);
		
		aim = true;
		int signal = 1;
		aimCamOffset.x = Mathf.Abs(aimCamOffset.x) * signal;
		aimPivotOffset.x = Mathf.Abs(aimPivotOffset.x) * signal;
		yield return new WaitForSeconds(0.1f);
		//behaviourManager.GetAnim.SetFloat(speedFloat, 0);
	}

	// Co-rountine to end aiming mode with delay.
	private IEnumerator ToggleAimOff()
	{
		aim = false;
		yield return new WaitForSeconds(0.3f);
		behaviourManager.GetCamScript.ResetTargetOffsets();
		behaviourManager.GetCamScript.ResetMaxVerticalAngle();
		yield return new WaitForSeconds(0.05f);
		//behaviourManager.RevokeOverridingBehaviour(this);
		kira.ActivateMovement(true);
	}
	
	// Rotate the player to match correct orientation, according to camera.
	void Rotating()
	{
		Vector3 forward = behaviourManager.playerCamera.TransformDirection(Vector3.forward);
		// Player is moving on ground, Y component of camera facing is not relevant.
		forward.y = 0.0f;
		forward = forward.normalized;

		// Always rotates the player according to the camera horizontal rotation in aim mode.
		Quaternion targetRotation =  Quaternion.Euler(0, behaviourManager.GetCamScript.GetH, 0);

		float minSpeed = Quaternion.Angle(transform.rotation, targetRotation) * aimTurnSmoothing;

		// Rotate entire player to face camera.
		behaviourManager.SetLastDirection(forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, minSpeed * Time.deltaTime);

	}

 	// Draw the crosshair when aiming.
	void OnGUI () 
	{
		if (crosshair)
		{
			float mag = behaviourManager.GetCamScript.GetCurrentPivotMagnitude(aimPivotOffset);
			if (mag < 0.05f)
				GUI.DrawTexture(new Rect(Screen.width / 2 - (crosshair.width * 0.5f),
										 Screen.height / 2 - (crosshair.height * 0.5f),
										 crosshair.width, crosshair.height), crosshair);
		}
	}
}
