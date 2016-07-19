using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	// Specify the components for the AI to use.
	public CharacterMotion motion = null;
	public CharacterCombat combat = null;

	// Specify a target for the AI to attack.
	public GameObject target = null;

	public CharacterState state = null;

	void Start()
	{
		Debug.Assert(motion != null && combat != null && state != null, "Missing references not set for enemy AI.");

		state.AddActionUpdate(CharacterState.Type.Idle, ProcessAction);
	}

	private void ProcessAction()
	{
		// Dummy just move towards target and attack when in range, plus only see in 1 dimension - horizontal.
		motion.MoveStop();
		if( target != null )
		{
			float distance = Mathf.Abs(target.transform.position.x - motion.transform.position.x);
			if( distance < 0.7f )
			{
				// Attack
				combat.WeaponAttack();
			}
			else
			{
				// Move
				if( target.transform.position.x > motion.transform.position.x )
				{
					motion.WalkRight();
				}
				else
				{
					motion.WalkLeft();
				}
			}
		}
	}
}
