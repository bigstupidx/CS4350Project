using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public float speed = 6f;            // The speed that the player will move at.
	public float closeToDestinationThreshold = 0.2f; // Player will stop moving when the distance from the destination is below this threshold
	Vector3 destination;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.
	bool isWalking;
	Vector3 lastMovement;
	int noOfFramesNotMoving = 0;
	int maxNoOfFramesNotMoving = 3;
	float noMovementThreshold = 0.005f;

	void Awake ()
	{
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		
		// Set up references.
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}



	void Update ()
	{
		// Check mouse input
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			// Create a ray from the mouse cursor on screen in the direction of the camera.
			Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			// Create a RaycastHit variable to store information about what was hit by the ray.
			RaycastHit floorHit;
			
			// Perform the raycast and if it hits something on the floor layer...
			if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
			{
				destination = floorHit.point; 

				// Create a vector from the player to the point on the floor the raycast from the mouse hit.
				Vector3 playerToMouse = floorHit.point - transform.position;
				
				// Ensure the vector is entirely along the floor plane.
				playerToMouse.y = 0f;
				
				// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
				Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
				
				// Set the player's rotation to this new rotation.
				playerRigidbody.MoveRotation (newRotation);

				isWalking = true;
			}
		}


		if(isWalking){


			// Movement
			Vector3 movement = destination - transform.position;

		
			float moveDifference = lastMovement.magnitude - movement.magnitude;
			Debug.Log (moveDifference);
			if(moveDifference < noMovementThreshold)
				noOfFramesNotMoving++;
			else
				noOfFramesNotMoving = 0;

			lastMovement = movement;
			movement = movement.normalized * speed * Time.deltaTime;
			playerRigidbody.MovePosition (transform.position + movement);

			// End movement if close to destination
			movement = destination - transform.position;
			if(movement.magnitude <= closeToDestinationThreshold){
				isWalking = false;
				noOfFramesNotMoving = 0;
			}

			//End walk if stuck for too many frames
			if(noOfFramesNotMoving > maxNoOfFramesNotMoving){
				isWalking = false;
				noOfFramesNotMoving = 0;
			}
		}

	
		anim.SetBool ("IsWalking", isWalking);
	}

}