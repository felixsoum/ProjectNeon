using UnityEngine;
using System.Collections.Generic;

public class CharacterCombat : MonoBehaviour
{
	// Specify cooldown between attacks
	public List<float> rangeCooldowns = new List<float>();

	// Specify the weapon to use.
	public CharacterWeapon weapon = null;

	// Specify projectile for range attack to spawn.
	public GameObject projectilePrefab = null;

	public CharacterState state = null;

	private float rangeCooldownTime = 0.0f;
	private int rangeCooldownIndex = 0;

	void Start()
	{
		Debug.Assert(weapon != null && state != null, "References not set for character combat.");
	}

	void Update()
	{
		if( rangeCooldownTime > 0.0f )
		{
			rangeCooldownTime -= Time.deltaTime;
		}
	}

	public void RangeAttack()
	{
		Debug.Assert(projectilePrefab != null, "Projectile reference not set for range attack with character combat.");

		if( rangeCooldownTime > 0.0f )
		{
			return;
		}

		if( rangeCooldowns.Count > 0 )
		{
			rangeCooldownTime = rangeCooldowns[rangeCooldownIndex];
			rangeCooldownIndex = (rangeCooldownIndex + 1) % rangeCooldowns.Count;
		}

		if ( projectilePrefab != null )
		{
			GameObject projectile = Instantiate(projectilePrefab, this.transform.position, this.transform.rotation) as GameObject;
			var projectileComponent = projectile.GetComponent<StraightProjectile>();
			projectileComponent.SetDirection(this.transform.localScale.x > 0.0f ? Vector3.right : Vector3.left);
		}
	}

	public void WeaponAttack()
	{
		if( state.Get() != CharacterState.Type.Attack )
		{
			state.Set(CharacterState.Type.Attack);
			weapon.PlayAnimation(CharacterWeapon.Animation.Attack, () => { state.Set(CharacterState.Type.Idle); });
		}
	}
	
}
