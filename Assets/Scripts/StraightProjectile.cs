using UnityEngine;
using System.Collections;

public class StraightProjectile : MonoBehaviour
{
	// Projectile speed in unity units per second.
	public float speed = 7.0f;

	private Vector3 direction = Vector3.right;

	public void SetDirection( Vector3 direction )
	{
		this.direction = direction;
	}

	void Update()
	{
		this.transform.position += direction.normalized * speed * Time.deltaTime;

		// Self destruct if going out of bounds.
		Camera cameraComponent = Camera.main;
		Vector3 cameraSizeHalf = new Vector3(cameraComponent.aspect * cameraComponent.orthographicSize, cameraComponent.orthographicSize, float.MaxValue);
		Bounds cameraBounds = new Bounds(cameraComponent.transform.position, cameraSizeHalf * 2.0f);
		if ( !cameraBounds.Contains(this.transform.position) )
		{
			Destroy(this.gameObject);
		}
	}
}
