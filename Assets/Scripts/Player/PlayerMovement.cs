using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	
	public float speed = 6f;            // The speed that the player will move at.
	public float closeToDestinationThreshold = 0.2f; // Player will stop moving when the distance from the destination is below this threshold
	Vector3 destination;                   // The vector to store the direction of the player's movement.
	bool mouseOverButton = false;


	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	SpriteRenderer spriteRenderer;		// Reference to player's spriteRenderer
	int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.



	// Movement Controls
	bool isWalking;
	Vector3 lastMovement;
	int noOfFramesNotMoving = 0;
	int maxNoOfFramesNotMoving = 3;
	float noMovementThreshold = 0.005f;

	// Sprite Frame Controls
	const int downOffset = 0;
	const int leftOffset = 3;
	const int rightOffset = 6;
	const int upOffset = 9;
	int currDirection =0;
	int currFrame = 1;
	bool flip = true;
	float currTime = 0;
	float timePerFrame = 0.1f;

	Sprite[] sprites;

	void Awake ()
	{
		// Create a layer mask for the floor layer.
		floorMask = LayerMask.GetMask ("Floor");
		
		// Set up references.
		spriteRenderer = GetComponent<SpriteRenderer> ();
		playerRigidbody = GetComponent<Rigidbody> ();

		// Load Sprite
		sprites = Resources.LoadAll<Sprite>(PlayerData.TextureName);
		spriteRenderer.sprite = sprites[currDirection + currFrame];

	}
	public void MouseOverButton(){
		mouseOverButton = true;
	}

	public void MouseLeftButton(){
		mouseOverButton = false;
	}

	void FixedUpdate ()
	{
		currTime += Time.deltaTime;
		// Check mouse input
		if (Input.GetKeyDown (KeyCode.Mouse0)&& PlayerData.MoveFlag && !mouseOverButton) {
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

				float angle = newRotation.eulerAngles.y;

				//Debug.Log(angle);


				currTime = timePerFrame;

				// Left
				if(angle>=225 && angle <= 315){
					currDirection = leftOffset;
				}
				//Right
				else if(angle>=45 && angle <= 135){
					currDirection = rightOffset;
				}
				// Up
				else if(angle>=315 || angle <= 45){
					currDirection = upOffset;
				}
				// Down
				else{
					currDirection = downOffset;
				}
				isWalking = true;
			}
		}


		if(isWalking){


			// Movement
			Vector3 movement = destination - transform.position;

		
			float moveDifference = lastMovement.magnitude - movement.magnitude;

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

		// Check for sprite frame update
		if (currTime >= timePerFrame) {
			if(isWalking){
				if(currFrame == 1){
					if(flip)
						currFrame = 0;
					else
						currFrame = 2;

					flip = !flip;

				}
				else{
					currFrame = 1;
				}
			}
			else{
				currFrame = 1;
				flip = true;
			}

			// Load New Frame
			spriteRenderer.sprite = sprites[currDirection + currFrame];
			currTime = 0;
		}

	}

}