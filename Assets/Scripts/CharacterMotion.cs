using UnityEngine;
using System.Collections.Generic;

public class CharacterMotion : MonoBehaviour
{
	// Specify the rigidbody for motions to apply to.
	public Rigidbody2D rigidbodyComponent = null;

	// Jump height in unity units.
	public float jumpHeight = 1.0f;
	// Jump time duration in air in seconds.
	public float jumpTime = 0.4f;
	// Move speed in unity units per second.
	public float walkSpeed = 3.0f;
	public float runSpeed = 6.0f;

	public CharacterState state = null;

	public LayerMask groundLayerMask;
	public float groundDistance = 0.35f;

	void Start()
	{
		Debug.Assert(rigidbodyComponent != null && state != null, "References not specified for character motion.");
		Jump();

		state.AddActionUpdate(CharacterState.Type.Jump, DetectFall);
		state.AddActionUpdate(CharacterState.Type.Fall, DetectGround);
	}

	// Vertical
	public void Jump()
	{
		// Must have non-zero jump time to jump, also gravity must be in Y axis
		if( jumpTime != 0.0f )
		{
			Debug.Assert(Physics2D.gravity.y != 0.0f, "Gravity must be in Y axis for character motion jump.");
			// Calculate the jump velocity and gravity based on height and time using kinematics equations.
			float jumpTimeHalf = 0.5f * jumpTime;
			float jumpVelocity = 2.0f * jumpHeight / jumpTimeHalf;
			rigidbodyComponent.gravityScale = (-jumpVelocity / jumpTimeHalf) / Physics2D.gravity.y;

			rigidbodyComponent.velocity = new Vector2(rigidbodyComponent.velocity.x, jumpVelocity);
			state.Set(CharacterState.Type.Jump);
		}
	}
	// Makes character start falling from jump immediately 
	public void JumpFall()
	{
		rigidbodyComponent.velocity = Vector2.zero;
		state.Set(CharacterState.Type.Fall);
	}

	// Horizontal
	public void WalkLeft()
	{
		SetFaceLeft();
		rigidbodyComponent.velocity = new Vector2(-walkSpeed, rigidbodyComponent.velocity.y);
	}
	public void WalkRight()
	{
		SetFaceRight();
		rigidbodyComponent.velocity = new Vector2(walkSpeed, rigidbodyComponent.velocity.y);
	}
	public void RunLeft()
	{
		SetFaceLeft();
		rigidbodyComponent.velocity = new Vector2(-runSpeed, rigidbodyComponent.velocity.y);
	}
	public void RunRight()
	{
		SetFaceRight();
		rigidbodyComponent.velocity = new Vector2(runSpeed, rigidbodyComponent.velocity.y);
	}
	public void MoveStop()
	{
		rigidbodyComponent.velocity = new Vector2(0.0f, rigidbodyComponent.velocity.y);
	}

	public void SetFaceLeft()
	{
		Vector3 scale = rigidbodyComponent.transform.localScale;
		rigidbodyComponent.transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
	}
	public void SetFaceRight()
	{
		Vector3 scale = rigidbodyComponent.transform.localScale;
		rigidbodyComponent.transform.localScale = new Vector3(+Mathf.Abs(scale.x), scale.y, scale.z);
	}

	private void DetectFall()
	{
		if( rigidbodyComponent.velocity.y <= 0.0f )
		{
			state.Set(CharacterState.Type.Fall);
		}
	}
	private void DetectGround()
	{
		// todo can specify the distance in editor visually via line collider
		var hits = new List<RaycastHit2D>(Physics2D.LinecastAll(this.rigidbodyComponent.position, this.rigidbodyComponent.position + Vector2.down * groundDistance, groundLayerMask.value));
		hits.RemoveAll(( h ) => { return h.rigidbody == rigidbodyComponent; });
		if( hits.Count > 0 )
		{
			state.Set(CharacterState.Type.Idle);
		}
	}
}
