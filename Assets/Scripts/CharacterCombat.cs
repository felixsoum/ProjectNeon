using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{
	// Specify the weapon to use.
	public CharacterWeapon weapon = null;

	// Specify projectile for range attack to spawn.
	public GameObject projectilePrefab = null;

	public CharacterState state = null;

	void Start()
	{
		Debug.Assert(weapon != null && state != null, "References not set for character combat.");
	}

	public void RangeAttack()
	{
		Debug.Assert(projectilePrefab != null, "Projectile reference not set for range attack with character combat.");
		if ( projectilePrefab != null )
		{
			GameObject projectile = Instantiate(projectilePrefab, this.transform.position, this.transform.rotation) as GameObject;
			var projectileComponent = projectile.GetComponent<StraightProjectile>();
			projectileComponent.SetDirection(this.transform.localScale.x > 0.0f ? Vector3.right : Vector3.left);
		}
	}

	public void WeaponAttack()
	{
		state.Set(CharacterState.Type.Attack);
		weapon.PlayAnimation(CharacterWeapon.Animation.Attack, () => { state.Set(CharacterState.Type.Idle); });
	}
	
}
