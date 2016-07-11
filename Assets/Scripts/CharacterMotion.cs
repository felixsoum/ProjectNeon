using UnityEngine;
using System.Collections;

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

	void Start()
	{
		Debug.Assert(rigidbodyComponent != null, "Rigidbody not specified for character motion.");
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
		}
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
}
