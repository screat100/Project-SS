﻿using UnityEngine;
using System.Collections;

// AimBehaviour inherits from GenericBehaviour. This class corresponds to aim and strafe behaviour.
public class AimBehaviour : GenericBehaviour
{
	public string aimButton = "Aim";     // Default aim buttons.
	public Texture2D crosshair;                                           // Crosshair texture.
	public float aimTurnSmoothing = 0.15f;                                // Speed of turn response when aiming to match camera facing.
	public Vector3 aimPivotOffset = new Vector3(0.5f, 1.2f,  0f);         // Offset to repoint the camera when aiming.
	public Vector3 aimCamOffset   = new Vector3(0f, 0.4f, -0.7f);         // Offset to relocate the camera when aiming.

	private int aimBool;                                                  // Animator variable related to aiming.
	private bool aim;                                                     // Boolean to determine whether or not the player is aiming.
	private Vector3 initialRootRotation;                                  // Initial root bone local rotation.
	private Vector3 initialHipsRotation;                                  // Initial hips rotation related to the root bone.
	private Vector3 initialSpineRotation;                                 // Initial spine rotation related to the root bone.
    Animator animator;
	// Start is always called after any Awake functions.
	void Start ()
	{
		// Set up the references.
		aimBool = Animator.StringToHash("Aim");
        animator = GetComponent<Animator>();
		// Get initial bone rotation values.
		Transform hips = animator.GetBoneTransform(HumanBodyBones.Hips);
		initialRootRotation = (hips.parent == transform) ? Vector3.zero : hips.parent.localEulerAngles;
		initialHipsRotation = hips.localEulerAngles;
		initialSpineRotation = animator.GetBoneTransform(HumanBodyBones.Spine).localEulerAngles;
	}

	// Update is used to set features regardless the active behaviour.
	void Update ()
	{

		// Activate/deactivate aim by input.
		if (Input.GetAxisRaw(aimButton) != 0 && !aim)
		{
			StartCoroutine(ToggleAimOn());
		}
		else if (aim && Input.GetAxisRaw(aimButton) == 0)
		{
			StartCoroutine(ToggleAimOff());
		}

		// No sprinting while aiming.
		canSprint = !aim;

        // Set aim boolean on the Animator Controller.
        animator.SetBool(aimBool, aim);
        //behaviourManager.GetAnim.SetBool (aimBool, aim);
	}

	// Co-rountine to start aiming mode with delay.
	private IEnumerator ToggleAimOn()
	{
		yield return new WaitForSeconds(0.05f);
		// Aiming is not possible.
		if (behaviourManager.GetTempLockStatus(this.behaviourCode) || behaviourManager.IsOverriding(this))
			yield return false;

		// Start aiming.
		else
		{
			aim = true;
			int signal = 1;
			aimCamOffset.x = Mathf.Abs(aimCamOffset.x) * signal;
			aimPivotOffset.x = Mathf.Abs(aimPivotOffset.x) * signal;
			yield return new WaitForSeconds(0.1f);
			//behaviourManager.GetAnim.SetFloat(speedFloat, 0);
			// This state overrides the active one.
			behaviourManager.OverrideWithBehaviour(this);
		}
	}

	// Co-rountine to end aiming mode with delay.
	private IEnumerator ToggleAimOff()
	{
		aim = false;
		yield return new WaitForSeconds(0.3f);
		//behaviourManager.GetCamScript.ResetTargetOffsets();
		//behaviourManager.GetCamScript.ResetMaxVerticalAngle();
		yield return new WaitForSeconds(0.05f);
		behaviourManager.RevokeOverridingBehaviour(this);
	}

	// LocalFixedUpdate overrides the virtual function of the base class.
	public override void LocalFixedUpdate()
	{
		// Set camera position and orientation to the aim mode parameters.
		//if(aim)
		//	behaviourManager.GetCamScript.SetTargetOffsets (aimPivotOffset, aimCamOffset);
	}

	// LocalLateUpdate: manager is called here to set player rotation after camera rotates, avoiding flickering.
	public override void LocalLateUpdate()
	{
		AimManagement();
	}

	// Handle aim parameters when aiming is active.
	void AimManagement()
	{
		// Deal with the player orientation when aiming.
		Rotating();
	}

	// Rotate the player to match correct orientation, according to camera.
	void Rotating()
	{
  //      Vector3 forward = transform.forward;
		//// Player is moving on ground, Y component of camera facing is not relevant.
		//forward.y = 0.0f;
		//forward = forward.normalized;

		//// Always rotates the player according to the camera horizontal rotation in aim mode.
		//Quaternion targetRotation =  Quaternion.Euler(0, transform.forward.x, 0);

		//float minSpeed = Quaternion.Angle(transform.rotation, targetRotation) * aimTurnSmoothing;

	
		////// Rotate entire player to face camera.
	 //   behaviourManager.SetLastDirection(forward);
		//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, minSpeed * Time.deltaTime);
		
	}


}
