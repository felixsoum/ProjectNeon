using UnityEngine;
using System.Collections;

public class MergeOtherTest : MonoBehaviour
{
	void Start()
    {
        Debug.Log("I'm active");
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        playerObject.transform.localScale *= 2f;
	}
}
