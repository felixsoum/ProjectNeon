using UnityEngine;
using System.Collections;

public class DeathTimer : MonoBehaviour
{

	// Use this for initialization
	void Start()
    {
        Invoke("Kill", 5f);
	}

    void Kill()
    {
        Destroy(gameObject);
    }

}
