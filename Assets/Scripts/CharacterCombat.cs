using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{
	// Specify the weapon to use.
	public CharacterWeapon weapon = null;

	// Specify projectile for range attack to spawn.
	public GameObject projectilePrefab = null;

	void Start()
	{
		Debug.Assert(weapon != null, "Weapon reference not set for character combat.");
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
		weapon.SetState(CharacterWeapon.State.Attack);
	}

	public bool IsAttacking()
	{
		return weapon.GetState() == CharacterWeapon.State.Attack; 
	}
}
