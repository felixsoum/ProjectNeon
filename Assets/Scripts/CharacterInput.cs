using UnityEngine;
using System.Collections.Generic;

public class CharacterInput : MonoBehaviour
{
	// Specify the components for the input to apply to.
	public CharacterMotion motion = null;
	public CharacterCombat combat = null;

	public CharacterState state = null;

	void Start()
	{
		Debug.Assert(motion != null && combat != null && state != null, "Missing references not set for character input.");

		state.AddActionUpdate(CharacterState.Type.Idle, ProcessJump);
		state.AddActionUpdate(CharacterState.Type.Jump, ProcessJumpFall);
		state.AddActionUpdate(CharacterState.Not(CharacterState.Type.Attack), ProcessMovement);
		state.AddActionUpdate(CharacterState.Not(CharacterState.Type.Attack), ProcessCombat);
		state.AddActionEnter(CharacterState.Type.Attack, () => { motion.MoveStop(); });
	}

	private void ProcessJumpFall()
	{
		if( Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) )
		{
			motion.JumpFall();
		}
	}
	private void ProcessJump()
	{
		if( Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) )
		{
			motion.Jump();
		}
	}
	private void ProcessMovement()
	{
		motion.MoveStop();
		if( Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) )
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
		if( Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) )
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
	private void ProcessCombat()
	{
		/* Disabled because combat controlled by rhythm
		if( Input.GetMouseButtonDown(0) )
		{
			combat.WeaponAttack();
		}
		if( Input.GetMouseButtonDown(1) )
		{
			combat.RangeAttack();
		}
		*/
	}
}
