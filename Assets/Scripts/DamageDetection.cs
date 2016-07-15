using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageDetection : MonoBehaviour
{
	// Use unity game object tags to determine what damages what.
	// Map character tag to a list of tags which will damage that character.
	private Dictionary<string, List<string>> damage_tags = new Dictionary<string, List<string>>
	{
		{ "Player", new List<string>() { "EnemyDamage" } },
		{ "Enemy",  new List<string>() { "PlayerDamage" } },
	};

	void Start()
	{
		Debug.Assert(damage_tags.ContainsKey(this.tag), "Game object tag is not defined for damage detection.");
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if( damage_tags[this.tag].Contains(other.tag) )
		{
			// todo notify things here
			Debug.Log(this + " DAMAGED BY " + other);

			// For now just teleport the object as if killed and respawned
			this.transform.position = Vector3.zero;
		}
	}
}
