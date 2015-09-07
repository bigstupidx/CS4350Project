using UnityEngine;
using System.Collections;

public class PlayerEyeMovement : MonoBehaviour {

	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene

	// Use this for initialization
	void Start () {
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
	}
	
	// Update is called once per frame
	void Update () {

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.

			Vector3 playerPosition = new Vector3();
			playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
			playerPosition.y=0;

			Vector3 mousePosition = floorHit.point;
			mousePosition.y =0;

			Vector3 playerToMouse = mousePosition -  playerPosition;

			float angle = Mathf.Atan2(playerToMouse.z,playerToMouse.x)*Mathf.Rad2Deg;

			Vector3 pos = new Vector3();
			float eyeStrength = Mathf.Min (1.0f,Mathf.Max (0.0f,playerToMouse.magnitude/6.0f));
			pos.x = Mathf.Cos (angle*Mathf.Deg2Rad)  * 0.01f * eyeStrength;
			pos.y = Mathf.Sin (angle*Mathf.Deg2Rad) * 0.01f * eyeStrength;
			pos.z = 0.0f;

			transform.localPosition  = pos;

		} else {
			transform.localPosition = new Vector3(0,0,0);
		}
	}
}
