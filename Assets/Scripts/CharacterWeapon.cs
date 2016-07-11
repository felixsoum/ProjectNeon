using UnityEngine;
using System.Collections;
using System;

public class CharacterWeapon : MonoBehaviour
{
	// State of weapon should match with animation states as well.
	public enum State
	{
		Idle,
		Attack,
	}

	// Specify the animator which has the animation states setup.
	public Animator weaponAnimator = null;

	void Start()
	{
		Debug.Assert(weaponAnimator != null, "Weapon animator reference not set for character weapon.");
	}

	public void SetState( State state )
	{
		weaponAnimator.Play(state.ToString(), -1, 0.0f);
	}

	public State GetState()
	{
		foreach( State state in Enum.GetValues(typeof(State)) )
		{
			if( weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName(state.ToString()) )
			{
				return state;
			}
		}
		Debug.Assert(false, "Could not find state of character weapon.");
		return State.Idle;
	}

}
