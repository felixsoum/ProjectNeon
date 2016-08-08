using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
	// Specify the movement path to follow.
	public EdgeCollider2D movePath = null;
	public bool movePathLooping = true;

	// Specify the combat ranges
	public Collider2D rangeArea = null;
	public Collider2D meleeArea = null;

	// Specify the components for the AI to use.
	public CharacterMotion motion = null;
	public CharacterCombat combat = null;

	// Specify a target for the AI to attack.
	public GameObject target = null;

	public EnemyState state = null;
	public CharacterState characterState = null;

	private List<Vector2> movePoints = new List<Vector2>();
	private int moveIndex = 0;
	private Vector2 moveDirection = Vector2.zero;

	void Start()
	{
		Debug.Assert(motion != null && combat != null && state != null && characterState != null, "Missing references not set for enemy AI.");

		if( movePath != null )
		{
			movePoints = new List<Vector2>(movePath.points);
			for( int i = 0; i < movePoints.Count; ++i )
			{
				movePoints[i] = new Vector2(Mathf.Round(movePoints[i].x * 100f) / 100f, movePoints[i].y);
				if( i == 0 )
				{
					motion.transform.position = movePoints[i];
				}
			}
			movePath.enabled = false;
		}

		state.AddActionUpdate(EnemyState.Type.Move, ProcessMove);
		state.AddActionUpdate(EnemyState.Type.Range, ProcessRange);
		state.AddActionUpdate(EnemyState.Type.Melee, ProcessMelee);
	}

	private void ProcessMove()
	{
		if( movePoints.Count > 0 )
		{
			Vector2 currentPoint = new Vector2(motion.transform.position.x, motion.transform.position.y);
			Vector2 nextPoint = movePoints[moveIndex];
			Vector2 lastDirection = moveDirection;

			moveDirection = nextPoint - currentPoint;
			if( (lastDirection.x * moveDirection.x) < 0.0f )
			{
				moveIndex = (moveIndex + 1) % movePoints.Count;
				if( moveIndex == 0 && !movePathLooping )
				{
					movePoints.Clear();
					return;
				}
				nextPoint = movePoints[moveIndex];
				moveDirection = nextPoint - currentPoint;
			}
			if( moveDirection.x > 0.0f )
			{
				motion.WalkRight();
			}
			else
			{
				motion.WalkLeft();
			}
		}
		ProcessArea();
	}

	private void ProcessRange()
	{
		motion.MoveStop();
		combat.RangeAttack();
		ProcessArea();
	}

	private void ProcessMelee()
	{
		combat.WeaponAttack();
		ProcessArea();
	}

	private void ProcessArea()
	{
		if( target != null )
		{
			var targetState = EnemyState.Type.Move;
			if( meleeArea.bounds.Contains(target.transform.position) )
			{
				targetState = EnemyState.Type.Melee;
			}
			else if( rangeArea.bounds.Contains(target.transform.position) )
			{
				targetState = EnemyState.Type.Range;
			}
			if( targetState != state.Get() )
			{
				state.Set(targetState);
			}
		}
	}
}
