using UnityEngine;
using System.Collections;
using System;

public class CharacterWeapon : MonoBehaviour
{
	// State of weapon should match with animation states as well.
	public enum Animation
	{
		Idle,
		Attack,
	}

	// Specify the animator which has the animation states setup.
	public Animator weaponAnimator = null;

	private Action onAnimationEnd = null;

	void Start()
	{
		Debug.Assert(weaponAnimator != null, "Weapon animator reference not set for character weapon.");
	}

	public void PlayAnimation( Animation animation, Action onAnimationEnd = null )
	{
		weaponAnimator.Play(animation.ToString(), -1, 0.0f);
		this.onAnimationEnd = onAnimationEnd;
	}

	private void OnAnimation_AttackEnd()
	{
		if (onAnimationEnd != null )
		{
			onAnimationEnd();
		}
	}
}
