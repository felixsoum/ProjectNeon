using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	// Specify the camera that will follow the target.
	public Camera cameraComponent = null;

	// Specify the target game object for the camera to follow.
	public GameObject targetObject = null;

	// Specify the box defining the extents in each axis where the camera will follow the target as they move away.
	public BoxCollider2D followBox = null;

	// Specify the box defining the area in which the camera is allowed to move within.
	public BoxCollider2D moveBox = null;

	void Start()
	{
		Debug.Assert(cameraComponent != null && targetObject != null && followBox != null && moveBox != null, "Missing references to be set on camera follow component.");
		Debug.Assert(cameraComponent.orthographic, "Camera is not in orthographic mode; camera follow will not work.");
	}

	void Update()
	{
		// Follow if target exceeds follow extents in respective axis
		Vector3 displacement = Vector3.zero;

		// Horizontal
		if( targetObject.transform.position.x > followBox.bounds.max.x )
		{
			displacement.x = targetObject.transform.position.x - followBox.bounds.max.x;
		}
		else if( targetObject.transform.position.x < followBox.bounds.min.x )
		{
			displacement.x = targetObject.transform.position.x - followBox.bounds.min.x;
		}

		// Vertical
		if( targetObject.transform.position.y > followBox.bounds.max.y )
		{
			displacement.y = targetObject.transform.position.y - followBox.bounds.max.y;
		}
		else if( targetObject.transform.position.y < followBox.bounds.min.y )
		{
			displacement.y = targetObject.transform.position.y - followBox.bounds.min.y;
		}

		Vector3 cameraPosition = cameraComponent.transform.position + displacement;

		// Make sure the camera stays within the move box boundaries.
		// Calculate bounds taking into account camera size extents.
		Vector3 cameraSizeHalf = new Vector3(cameraComponent.aspect * cameraComponent.orthographicSize, cameraComponent.orthographicSize, 0.0f);
		Bounds cameraBounds = new Bounds(moveBox.bounds.center, moveBox.bounds.size - cameraSizeHalf * 2.0f);

		cameraPosition.x = Mathf.Clamp(cameraPosition.x, cameraBounds.min.x, cameraBounds.max.x);
		cameraPosition.y = Mathf.Clamp(cameraPosition.y, cameraBounds.min.y, cameraBounds.max.y);

		cameraComponent.transform.position = cameraPosition;
	}
}
