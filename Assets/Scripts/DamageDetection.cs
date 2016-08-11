using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageDetection : MonoBehaviour
{
    const int PLAYER_HP_MAX = 5;
    int playerHp = PLAYER_HP_MAX;
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

			//Ghetto handling
			if(this.CompareTag("Player"))
			{
                if (--playerHp == 0)
                {
                    gameObject.SetActive(false);
                    Invoke("Respawn", 3f);
                }
            }
			else
			{
				Destroy(this.gameObject);
			}
		}
	}

    public int GetPlayerHp()
    {
        return playerHp;
    }

    void Respawn()
    {
        // For now just teleport the object as if killed and respawned
        gameObject.SetActive(true);
        this.transform.position = Vector3.zero;
        playerHp = PLAYER_HP_MAX;
    }
}
