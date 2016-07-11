using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{
	// Specify the weapon to use.
	public CharacterWeapon weapon = null;

	void Start()
	{
		Debug.Assert(weapon != null, "Weapon reference not set for character combat.");
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
