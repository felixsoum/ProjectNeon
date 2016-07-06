using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour
{
	// Specify the components for the input to apply to.
	public CharacterMotion motion = null;
	public CharacterCombat combat = null;

	void Start()
	{
		Debug.Assert(motion != null && combat != null, "Missing references not set for character input.");
	}

	void Update()
	{
		// todo: make input keys re-bindable
		if( Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) )
		{
			motion.Jump();
		}

		motion.MoveStop();
		if( Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) )
		{
			if( Input.GetKey(KeyCode.LeftShift) )
			{
				motion.WalkLeft();
			}
			else
			{
				motion.RunLeft();
			}
		}
		if( Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) )
		{
			if( Input.GetKey(KeyCode.LeftShift) )
			{
				motion.WalkRight();
			}
			else
			{
				motion.RunRight();
			}
		}
	}
}
